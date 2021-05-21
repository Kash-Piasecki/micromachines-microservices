using System;
using Automatonymous;

namespace MMStock.StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        
    }

    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
    }
}