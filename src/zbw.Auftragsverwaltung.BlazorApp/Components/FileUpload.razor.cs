using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tewr.Blazor.FileReader;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class FileUpload
    {
        private ElementReference _input;

        [Parameter]
        public bool MultipleFiles { get; set; }

        [Parameter]
        public EventCallback<List<FileContainer>> OnChange { get; set; }

        [Parameter]
        public bool UseDropZone { get; set; }

        [Parameter] public string Text { get; set; } = "Upload";

        protected override void OnParametersSet()
        {

        }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        private async Task HandleSelected()
        {
            var result = new List<FileContainer>();

            foreach (var fileReference in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (fileReference == null) continue;
                var fileInfo = await fileReference.ReadFileInfoAsync();
                var file = new FileContainer()
                {
                    FileName = fileInfo.Name,
                    ContentType = MediaTypeHeaderValue.Parse(fileInfo.Type).MediaType,
                    Content = await fileReference.CreateMemoryStreamAsync()
                };
                result.Add(file);

                if (!MultipleFiles)
                    break;
            }
            await OnChange.InvokeAsync(result);
        }

	}
}
