using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Infra.Mapping
{
    public class SubscribeFileMap : IEntityTypeConfiguration<SubscribeFile>
    {
        public void Configure(EntityTypeBuilder<SubscribeFile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Subscribe).WithMany().HasForeignKey(x => x.SubscribeId);
            builder.HasOne(x => x.File).WithMany().HasForeignKey(x => x.FileId);
        }
    }
}
