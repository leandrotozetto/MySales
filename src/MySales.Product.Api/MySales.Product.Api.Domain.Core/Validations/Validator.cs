using MySales.Product.Api.Domain.Core.Exceptions;
using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Core.Validations
{

    /// <summary>
    /// Class that has methods of validations for the entities.
    /// </summary>
    /// <typeparam name="T">Entity type to be validated.</typeparam>
    public class Validator<T> : IValidator<T>, IValidatorResult where T : class
    {
        private ICollection<IRuleBuilder<T>> _ruleBuilders;

        /// <summary>
        /// List with erros messages.
        /// </summary>
        public IEnumerable<DomainError> Errors => _ruleBuilders?.SelectMany(x => x.Errors) ?? Enumerable.Empty<DomainError>();

        public ICollection<IRule> Rules { get; private set; }

        public bool IsValid { get; private set; }

        private Validator() { }

        public static IValidator<T> New()
        {
            return new Validator<T>
            {
                Rules = new Collection<IRule>(),
                _ruleBuilders = new Collection<IRuleBuilder<T>>()
            };
        }

        public IRuleBuilder<T> RuleFor(Expression<Func<T, dynamic>> expressionProperty)
        {
            var ruleBuilder = RuleBuilder<T>.New(expressionProperty);
            _ruleBuilders.Add(ruleBuilder);

            return ruleBuilder;
        }

        public IRuleBuilder<T> RuleFor(Expression<Func<T, dynamic>> expressionProperty, string customNameField)
        {
            var ruleBuilder = RuleBuilder<T>.New(expressionProperty, customNameField);
            _ruleBuilders.Add(ruleBuilder);

            return ruleBuilder;
        }

        public IValidatorResult Validate(T entity)
        {
            var ruleBuilders = _ruleBuilders?.Where(x => x.PropertiesValidations.Any());

            foreach (var ruleBuilder in ruleBuilders)
            {
                if (ruleBuilder.IsValidRule(entity) && (ruleBuilder?.PropertiesValidations?.Any() ?? false))
                {
                    foreach (var propertieValidation in ruleBuilder.PropertiesValidations)
                    {
                        var propertyValues = propertieValidation.Compile(entity);

                        foreach (var rule in propertieValidation.Rules)
                        {
                            rule.Validate(ruleBuilder.CustomNameField ?? propertyValues.FieldName, propertyValues.FieldValue);
                        }
                    }
                }
            }

            IsValid = !Errors.Any();

            if (!IsValid)
            {
                throw new DomainException(Errors);
            }

            return this;
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
