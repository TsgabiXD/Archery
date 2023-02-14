using Archery.Model;

namespace Archery.Repository;

public abstract class AbstractRepository
{
    public ArcheryContext Context;

    public AbstractRepository(ArcheryContext context)
    {
        Context = context;
        //test
        //test2
    }
}