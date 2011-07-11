using Documently.Domain.Events;
using Raven.Client;

namespace Documently.ReadModel
{
	public class CustomerAddressView : HandlesEvent<CustomerCreatedEvent>, HandlesEvent<CustomerRelocatedEvent>
	{
		private readonly IDocumentStore _documentStore;

		public CustomerAddressView(IDocumentStore documentStore)
		{
			_documentStore = documentStore;
		}

		public void Consume(CustomerRelocatedEvent @event)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = session.Load<CustomerAddressDto>(Dto.GetDtoIdOf<CustomerAddressDto>(@event.AggregateId));
				dto.Street = @event.Street;
				dto.StreetNumber = @event.StreetNumber;
				dto.PostalCode = @event.PostalCode;
				dto.City = @event.City;
				session.SaveChanges();
			}
		}

		public void Consume(CustomerCreatedEvent @event)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = new CustomerAddressDto
				{
					AggregateRootId = @event.AggregateId,
					CustomerName = @event.CustomerName,
					Street = @event.Street,
					StreetNumber = @event.StreetNumber,
					PostalCode = @event.PostalCode,
					City = @event.City
				};
				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}