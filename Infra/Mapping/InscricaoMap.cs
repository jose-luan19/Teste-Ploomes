using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reality_subscribe_api.Model;

namespace Infra.Mapping
{
    public class InscricaoMap : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
