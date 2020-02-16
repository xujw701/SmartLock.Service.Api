using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class Property
    {
        public int PropertyId { get; private set; }
        public int CompanyId { get; private set; }
        public int BranchId { get; private set; }
        public string PropertyName { get; private set; }
        public string Address { get; private set; }
        public string Notes { get; private set; }
        public string Price { get; private set; }
        public double? Bedrooms { get; private set; }
        public double? Bathrooms { get; private set; }
        public double? FloorArea { get; private set; }
        public double? LandArea { get; private set; }
        public DateTime StartedOn { get; private set; }
        public DateTime EndedOn { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private Property(KeyboxPropertyCreateCommand command)
        {
            CompanyId = command.CompanyId;
            BranchId = command.BranchId;
            PropertyName = command.PropertyName;
            Address = command.Address;
            Notes = command.Notes;
            Price = command.Price;
            Bedrooms = command.Bedrooms;
            Bathrooms = command.Bathrooms;
            FloorArea = command.FloorArea;
            LandArea = command.LandArea;
        }

        private Property(KeyboxPropertyUpdateCommand command)
        {
            PropertyId = command.PropertyId;
            CompanyId = command.CompanyId;
            BranchId = command.BranchId;
            PropertyName = command.PropertyName;
            Address = command.Address;
            Notes = command.Notes;
            Price = command.Price;
            Bedrooms = command.Bedrooms;
            Bathrooms = command.Bathrooms;
            FloorArea = command.FloorArea;
            LandArea = command.LandArea;
        }

        private Property(PropertySnapshot snapshot)
        {
            PropertyId = snapshot.PropertyId;
            CompanyId = snapshot.CompanyId;
            BranchId = snapshot.BranchId;
            PropertyName = snapshot.PropertyName;
            Address = snapshot.Address;
            Notes = snapshot.Notes;
            Price = snapshot.Price;
            Bedrooms = snapshot.Bedrooms;
            Bathrooms = snapshot.Bathrooms;
            FloorArea = snapshot.FloorArea;
            LandArea = snapshot.LandArea;
            StartedOn = snapshot.StartedOn;
            EndedOn = snapshot.EndedOn;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static Property CreateFrom(KeyboxPropertyCreateCommand command)
        {
            return new Property(command);
        }

        public static Property CreateFrom(KeyboxPropertyUpdateCommand command)
        {
            return new Property(command);
        }

        public static Property CreateFrom(PropertySnapshot snapshot)
        {
            return new Property(snapshot);
        }
    }
}
