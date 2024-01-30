using Domain.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Domain
{

    public class Image : DomainEntityId
    {
        public Restaurant? Restaurant { get; set; }
        
        public Food? Food { get; set; }
        public string? Url { get; set; }

        public bool IsMain { get; set; }

        public bool IsApproved { get; set; }

        public EntityType EntityType { get; set; }
    }

    public enum EntityType
    {
        Restaurant,
        Food
    }
}