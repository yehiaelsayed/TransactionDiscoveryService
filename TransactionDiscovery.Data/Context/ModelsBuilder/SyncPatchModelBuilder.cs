using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionDiscovery.Data.Models;

namespace TransactionDiscovery.Data.Context.ModelsBuilder
{
    class SyncPatchModelBuilder : IEntityTypeConfiguration<DiscoveryPatch>
    {
        public void Configure(EntityTypeBuilder<DiscoveryPatch> builder)
        {
            builder.HasOne(S => S.Account);
        }
    }
}
