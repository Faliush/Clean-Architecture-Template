﻿using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.EntityConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

internal sealed class UserConfiguration : EntityConfiguration<User>
{
    protected override void AddBuilder(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(user => user.FirstName, firstNameBuider =>
        {
            firstNameBuider.WithOwner();

            firstNameBuider.Property(x => x.Value)
                .HasColumnName(nameof(FirstName))
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(x => x.Value)
                .HasColumnName(nameof(LastName))
                .HasMaxLength(LastName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(x => x.Value)
                .HasColumnName(nameof(Email))
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
        });

        builder.Property(user => user.PasswordHash).IsRequired();

        builder.Property(user => user.CreatedOnUtc).IsRequired();

        builder.Property(user => user.ModifiedOnUtc);

        builder.Ignore(user => user.FullName);

        builder.HasMany(x => x.Roles)
            .WithOne();
    }

    protected override string TableName() => "Users";
}