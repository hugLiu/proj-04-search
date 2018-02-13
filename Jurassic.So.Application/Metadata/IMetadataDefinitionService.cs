using System.Collections.Generic;

namespace Jurassic.So.Application.Metadata
{
    public interface IMetadataDefinitionService
    {
        List<MetadataDefinition> GetAllMetadatadefines();
    }
}