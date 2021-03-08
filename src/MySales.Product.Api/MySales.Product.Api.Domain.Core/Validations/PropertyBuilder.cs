//using MySales.Product.Api.Domain.Core.Validations.Interfaces;
//using System;
//using System.Linq.Expressions;

//namespace MySales.Product.Api.Domain.Core.Validations
//{
//    public class PropertyBuilder<T> : IPropertyBuilder<T> where T : class
//    {
//        private IRuleBuilder<T> _ruleBuilder;

//        private PropertyBuilder() { }

//        public static IPropertyBuilder<T> New(IRuleBuilder<T> ruleBuilder)
//        {
//            return new PropertyBuilder<T>
//            {
//                _ruleBuilder = ruleBuilder
//            };
//        }

//        //public IRuleBuilder<T> RuleFor(Expression<Func<T, dynamic>> expressionProperty)
//        //{
//        //    _ruleBuilder.AddValidation(expressionProperty);

//        //    return _ruleBuilder;
//        //}
//    }
//}
