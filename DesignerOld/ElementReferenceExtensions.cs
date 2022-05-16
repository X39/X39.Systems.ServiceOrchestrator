using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer;

public static class ElementReferenceExtensions
{
    public static ValueTask<ElementSize> GetSize(this ElementReference elementReference, IJSRuntime jsRuntime)
    {
        return jsRuntime.InvokeAsync<ElementSize>(
            "ElementReferenceExtensions_GetSize",
            elementReference);
    }
}