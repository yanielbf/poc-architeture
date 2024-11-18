using System.Net.Http.Headers;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients;
using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Attachemnts;

namespace WROBoxLabelGeneration.Infrastructure.HttpClients
{
    public class AttachmentsProxy : BaseProxy, IAttachmentsProxy
    {
        private readonly HttpClient _httpClient;
        
        private const string _endpointAttachment = "/api/attachment";

        public AttachmentsProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreatedAttachmentResponseDTO> CreateAttachmentsAsync(CreateAttachmentRequestDTO attachmentsToCreate)
        {
            var fileName = attachmentsToCreate.Name.Substring(0, attachmentsToCreate.Name.LastIndexOf('.'));
            var urlString = $"{_endpointAttachment}?domainType=10&fileType=2&blobKey=&fileName={fileName}";
            var url = new Uri(urlString, UriKind.Relative);
            using MultipartFormDataContent form = new MultipartFormDataContent();
            ByteArrayContent byteArrayContent = new ByteArrayContent(attachmentsToCreate.Data);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = byteArrayContent
            };
            var response = await _httpClient.SendAsync(requestMessage);
            return await ProcessResponseAsync<CreatedAttachmentResponseDTO>(response, _endpointAttachment, "");
        }
    }
}
