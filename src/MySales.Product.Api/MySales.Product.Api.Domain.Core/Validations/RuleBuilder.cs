using MySales.Product.Api.Domain.Core.Exceptions;
using MySales.Product.Api.Domain.Core.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace MySales.Product.Api.Domain.Core.Validations
{
    public class RuleBuilder<T> : IRuleBuilder<T> where T : class
    {
        public Collection<IPropertyValidation<T>> PropertiesValidations { get; private set; }

        /// <summary>
        /// List with erros messages.
        /// </summary>
        public IEnumerable<DomainError> Errors => _errors?.Select(x => DomainError.New(x.Key, x.Value));

        private IDictionary<string, ICollection<string>> _errors;

        public string CustomNameField { get; private set; }

        private Expression<Func<T, bool>> _condition;

        private RuleBuilder() { }

        public static IRuleBuilder<T> New(Expression<Func<T, dynamic>> expressionProperty)
        {
            return new RuleBuilder<T>
            {
                PropertiesValidations = new Collection<IPropertyValidation<T>>
                {
                    PropertyValidation<T>.New(expressionProperty)
                },
                _errors = new Dictionary<string, ICollection<string>>()
            };
        }

        public static IRuleBuilder<T> New(Expression<Func<T, dynamic>> expressionProperty, string customNameField)
        {
            return new RuleBuilder<T>
            {
                PropertiesValidations = new Collection<IPropertyValidation<T>>
                {
                    PropertyValidation<T>.New(expressionProperty)
                },
                _errors = new Dictionary<string, ICollection<string>>(),
                CustomNameField = customNameField
            };
        }

        public void AddErrorMessage(string name, string message)
        {
            _errors.TryGetValue(name, out var messages);

            if (messages == null)
            {
                messages = new Collection<string> { message };
                _errors.Add(name, messages);
            }
            else
            {
                _errors[name] = messages;
            }
        }

        //public void AddValidation(Expression<Func<T, dynamic>> expressionProperty)
        //{
        //    _propertiesValidations
        //        .Add(PropertyValidation<T>.New(expressionProperty));
        //}

        public void AddRule(IRule rule)
        {
            var count = PropertiesValidations.Count();
            var currentIndex = count == 0 ? 0 : count - 1;

            PropertiesValidations[currentIndex]
                .Rules
                .Add(rule);
        }

        //public void Validate(T entity)
        //{
        //    if (_propertiesValidations.Any())
        //    {
        //        _errors.Clear();

        //        foreach (var propertieValidation in _propertiesValidations)
        //        {
        //            var propertyValues = propertieValidation.Compile(entity);

        //            foreach (var rule in propertieValidation.Rules)
        //            {
        //                rule.Validate(propertyValues.FieldName, propertyValues.FieldValue);
        //            }
        //        }
        //    }

        //    if (_errors.Count > 0)
        //    {
        //        throw new DomainException(Errors);
        //    }
        //}

        //private void GetPropertyData()
        //{
        //    //var memberExpression = GetMemberExpression();
        //    //FieldName =  memberExpression.Member.Name;
        //    //var propInfo = memberExpression.Member as PropertyInfo;
        //    //dynamic value = propInfo.GetValue(entity, null);


        //    var t = Measure.New("Teste", true, System.Guid.NewGuid());

        //    Expression<Func<Measure, string>> teste = x => x.Name;

        //    var member = (MemberExpression)teste.Body;
        //    string propertyName = member.Member.Name;
        //    var func = teste.Compile();
        //    var value = func(t);
        //}

        public void When(Expression<Func<T, bool>> condition)
        {
            _condition = condition;
        }

        public bool IsValidRule(T entity)
        {
            if(_condition is null)
            {
                return true;
            }

            var func = _condition.Compile();

            return func.Invoke(entity);
        }
    }
}
