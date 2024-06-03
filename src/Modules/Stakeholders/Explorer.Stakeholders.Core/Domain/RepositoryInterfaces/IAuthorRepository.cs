using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Author GetByUserId(long userId);
        Author Update(Author author);
        List<Author> GetAll();
    }
}
