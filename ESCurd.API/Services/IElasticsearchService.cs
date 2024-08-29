namespace ESCurd.API.Services
{
    public interface IElasticsearchService<T>
    {
        Task<String> CreateDocumentAsync(T document);
        Task<T> GetDocumentAsync(int id);
        Task<IEnumerable<T> > GetAllDocuments();
        Task<String> UpdateDocumentAsync(T document);
        Task<String> DeleteDocumentAsync(int id);
    }
}
