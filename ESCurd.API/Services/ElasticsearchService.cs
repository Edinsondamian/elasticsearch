using Nest;
using System.Reflection.Metadata;

namespace ESCurd.API.Services
{
    public class ElasticsearchService<T> : IElasticsearchService<T> where T : class
    {
        private readonly ElasticClient _elasticClient;
        public ElasticsearchService(ElasticClient elasticClient)
        {
            _elasticClient= elasticClient;
        }
        public async Task<string> CreateDocumentAsync(T document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            return response.IsValid ? "Documento creado satisfactoriamente" : "Fallò la creaciòn del documento";
        }

        public async Task<string> DeleteDocumentAsync(int id)
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
            return response.IsValid ? "Documento eliminado satisfactoriamente" : "Fallò la eliminaciòn del documento";
        }

        public async Task<IEnumerable<T>> GetAllDocuments()
        {
            var searchResponse = _elasticClient.Search<T>(s => s.MatchAll().Size(10000));
            return searchResponse.Documents;
        }

        public async Task<T> GetDocumentAsync(int id)
        {
            var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));
            return response.Source;
        }

        public async Task<string> UpdateDocumentAsync(T document)
        {
            var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(document), u=>u
            .Doc(document).RetryOnConflict(3));

            return response.IsValid ? "Documento actualizado satisfactoriamente" : "Fallò la actualización del documento";
        }
    }
}
