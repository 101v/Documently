using System;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.Domain;
using CQRSSample.Domain.Events;
using Documently.Commands;
using NUnit.Framework;

namespace CQRSSample.Specs.Customers
{
	public class when_creating_a_new_customer :
		CommandTestFixture<CreateCustomerCommand, CreateCustomerCommandHandler, Customer>
	{
		protected override CreateCustomerCommand When()
		{
			return new CreateCustomerCommand(Guid.NewGuid(), "J�rg Egretzberger", "Ringstra�e", "1", "1010", "Wien", "01/123456");
		}

		[Test]
		public void Then_a_client_created_event_will_be_published()
		{
			Assert.AreEqual(typeof (CustomerCreatedEvent), PublishedEvents.Last().GetType());
		}

		[Test]
		public void Then_the_published_event_will_contain_the_name_of_the_client()
		{
			Assert.That(PublishedEvents.Last<CustomerCreatedEvent>().CustomerName == "J�rg Egretzberger");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_address_of_the_client()
		{
			PublishedEvents.Last<CustomerCreatedEvent>().Street.WillBe("Ringstra�e");
			PublishedEvents.Last<CustomerCreatedEvent>().StreetNumber.WillBe("1");
			PublishedEvents.Last<CustomerCreatedEvent>().PostalCode.WillBe("1010");
			PublishedEvents.Last<CustomerCreatedEvent>().City.WillBe("Wien");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_phone_number_of_the_client()
		{
			PublishedEvents.Last<CustomerCreatedEvent>().PhoneNumber.WillBe("01/123456");
		}
	}
}