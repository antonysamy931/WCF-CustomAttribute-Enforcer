using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Enforcer.Service
{
    public abstract class EnforcerBehavior : Attribute, IParameterInspector, IOperationBehavior
    {
        public virtual object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                if (this.Validate(operationName, inputs))
                {
                    var obj = inputs[0] as IData;
                    var obj1 = inputs[0] as ISample;
                    return null;
                }
            }
            catch
            {
                throw new FaultException("Access denied");
            }            
            throw new FaultException("Access denied");
        }

        public virtual void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public virtual void Validate(OperationDescription operationDescription)
        {
        }

        public virtual void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public virtual void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public virtual void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public abstract bool Validate(string operationName, object[] inputs);
    }

    public class TestEnforcer : EnforcerBehavior
    {
        public override bool Validate(string operationName, object[] inputs)
        {
            return true;
        }
    }
}