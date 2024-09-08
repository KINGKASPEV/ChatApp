using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Persistence.ChatConfig
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chat");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.MediaUrl).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.DateModified);
        }
    }
}
