//using MySales.Product.Api.Domain.Core.Exceptions;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;

//namespace MySales.Product.Api.Domain.Core.Entities
//{
//    public class Response<T> : BaseResponse, IResponse<T>
//    {
//        private static IResponse<T> _success;

//        /// <summary>
//        /// Return data.
//        /// </summary>
//        public T Result { get; private set; }

//        static Response()
//        {
//            _success ??= new Response<T>()
//            {
//                HasError = true,
//                Result = default
//            };
//        }

//        public static IResponseError Error(DomainError domainError)
//        {
//            var response = new Response<T>()
//            {
//                Result = default
//            };

//            response.AddError(domainError);

//            return response;
//        }

//        public static IResponse<T> Success()
//        {
//            return _success;
//        }

//        public static IResponse<T> Success(T result)
//        {
//            return new Response<T>()
//            {
//                HasError = true,
//                Result = result
//            };
//        }
//    }

//    public class BaseResponse : IResponseError
//    {
//        private List<DomainError> _errors;

//        /// <summary>
//        /// Operation's status
//        /// </summary>
//        public bool HasError { get; protected set; }

//        public IEnumerable<DomainError> Errors { get { return _errors.AsReadOnly(); } }

//        protected BaseResponse()
//        {
//            _errors = new List<DomainError>();
//        }

//        /// <summary>
//        /// Add error messages.
//        /// </summary>
//        /// <param name="domainError">Domain error.</param>
//        public IResponseError AddError(DomainError domainError)
//        {
//            if (domainError != null)
//            {
//                var error = _errors?.FirstOrDefault(x => x.Property == domainError.Property);

//                if (error != null)
//                {
//                    foreach (var message in domainError.Messages)
//                    {
//                        error.Messages.Add(message);
//                    }

//                    return this;
//                }

//                _errors.Add(domainError);
//            }

//            return this;
//        }
//    }

//    public class EmptyResponse : BaseResponse, IEmptyResponse
//    {
//        private EmptyResponse() { }

//        public static IResponseError Error(DomainError domainError)
//        {
//            var response = new EmptyResponse() { };

//            response.AddError(domainError);

//            return response;
//        }

//        public static IEmptyResponse Success()
//        {
//            return new EmptyResponse();
//        }

//        public static IEmptyResponse Information(string property, string message)
//        {
//            var domainError = DomainError.New(property, message);

//            return Information(domainError);
//        }

//        public static IEmptyResponse Information(DomainError domainError)
//        {
//            var emptyResponse = new EmptyResponse()
//            {
//                HasError = true
//            };

//            emptyResponse.AddError(domainError);

//            return emptyResponse;
//        }
//    }

//    public interface IEmptyResponse : IResponseError
//    {
//    }

//    public interface IResponseError
//    {
//        /// <summary>
//        /// Operation's status
//        /// </summary>
//        public bool HasError { get; }

//        IResponseError AddError(DomainError domainError);

//        IEnumerable<DomainError> Errors { get; }
//    }

//    public interface IResponse<T> : IResponseError
//    {
//        /// <summary>
//        /// Return data.
//        /// </summary>
//        T Result { get; }
//    }

//    public class ResponseEmpty { }


//    public class DomainEvent<T> : IDomainEvent<T>
//    {
//        public IEventStatus Status { get; private set; }

//        public bool IsEmpty { get; private set; }

//        public T Result { get; private set; }

//        public bool HasError { get; private set; }

//        public IReadOnlyCollection<DomainError> Errors
//        {
//            get
//            {
//                return _erros.AsReadOnly();
//            }
//        }

//        private List<DomainError> _erros;

//        private DomainEvent()
//        {
//            _erros = new List<DomainError>();
//        }

//        public IDomainEvent<T> New()
//        {
//            return new DomainEvent<T>();
//        }

//        public static IDomainEvent<T> Success(T result)
//        {
//            return new DomainEvent<T>()
//            {
//                Result = result,
//                IsEmpty = false,
//                HasError = false,
//                Status = EventStatusEnum.Success
//            };
//        }

//        public static IDomainEvent<T> Information(DomainError error)
//        {
//            var domainEvent = new DomainEvent<T>()
//            {
//                Result = default,
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Information
//            };

//            domainEvent._erros.Add(error);

//            return domainEvent;
//        }

//        public static IDomainEvent<T> Information(string propertyName, string message)
//        {
//            var domainEvent = new DomainEvent<T>()
//            {
//                Result = default,
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Information
//            };

//            var domainError = DomainError.New(propertyName, message);

//            domainEvent._erros.Add(domainError);

//            return domainEvent;
//        }
//    }

//    public class EmptyDomainEvent : IEmptyDomainEvent
//    {
//        public IEventStatus Status { get; private set; }

//        public bool IsEmpty { get; private set; }

//        public bool HasError { get; private set; }

//        public IReadOnlyCollection<DomainError> Errors
//        {
//            get
//            {
//                return _erros.AsReadOnly();
//            }
//        }

//        private List<DomainError> _erros;

//        private EmptyDomainEvent()
//        {
//            _erros = new List<DomainError>();
//        }

//        public IEmptyDomainEvent New()
//        {
//            return new EmptyDomainEvent();
//        }

//        public static IEmptyDomainEvent Empty()
//        {
//            return new EmptyDomainEvent()
//            {
//                IsEmpty = true,
//                HasError = false,
//                Status = EventStatusEnum.Success
//            };
//        }

//        public static IEmptyDomainEvent Information(DomainError error)
//        {
//            var domainEvent = new EmptyDomainEvent()
//            {
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Information
//            };

//            domainEvent._erros.Add(error);

//            return domainEvent;
//        }

//        public static IEmptyDomainEvent Information(string propertyName, string message)
//        {
//            var domainEvent = new EmptyDomainEvent()
//            {
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Information
//            };

//            var domainError = DomainError.New(propertyName, message);

//            domainEvent._erros.Add(domainError);

//            return domainEvent;
//        }

//        public static IEmptyDomainEvent Error(DomainError error)
//        {
//            var domainEvent = new EmptyDomainEvent()
//            {
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Error
//            };

//            domainEvent._erros.Add(error);

//            return domainEvent;
//        }

//        public static IEmptyDomainEvent Error(string propertyName, string message)
//        {
//            var domainEvent = new EmptyDomainEvent()
//            {
//                IsEmpty = true,
//                HasError = true,
//                Status = EventStatusEnum.Information
//            };

//            var domainError = DomainError.New(propertyName, message);

//            domainEvent._erros.Add(domainError);

//            return domainEvent;
//        }
//    }

//    public interface IDomainEvent<T> : IEmptyDomainEvent
//    {
//        T Result { get; }
//    }

//    public interface IEmptyDomainEvent
//    {
//        IEventStatus Status { get; }

//        bool IsEmpty { get; }

//        bool HasError { get; }

//        IReadOnlyCollection<DomainError> Errors { get; }
//    }

//    public interface IEventStatus
//    {
//        public string Value { get; }
//    }

//    public class EventStatusEnum
//    {
//        public static IEventStatus Success = EventStatus.New("SUCCESS");

//        public static IEventStatus Error = EventStatus.New("ERROR");

//        public static IEventStatus Information { get; } = EventStatus.New("INFORMATION");

//        public class EventStatus : IEventStatus
//        {
//            public string Value { get; private set; }

//            private EventStatus() { }

//            public static IEventStatus New(string type)
//            {
//                return new EventStatus()
//                {
//                    Value = type
//                };
//            }
//        }
//    }

//    //public class SuccessEventStatus : IEventStatus
//    //{
//    //    public int EventId { get; }

//    //    public string EventType { get; private set; }

//    //    private SuccessEventStatus() { }

//    //    public static SuccessEventStatus New()
//    //    {
//    //        return new SuccessEventStatus()
//    //        {
//    //            EventType = "SUCCESS"
//    //        };
//    //    }
//    //}

//    //public class ErrorEventStatus : IEventStatus
//    //{
//    //    public int EventId => throw new System.NotImplementedException();

//    //    public string EventType => throw new System.NotImplementedException();
//    //}

//    //public class InformationEventStatus : IEventStatus
//    //{
//    //    public int EventId => throw new System.NotImplementedException();

//    //    public string EventType => throw new System.NotImplementedException();
//    //}

//    //https://refactoring.guru/design-patterns/observer/csharp/example
//}
