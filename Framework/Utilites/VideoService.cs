using Microsoft.JSInterop;

namespace Framework.Video;

public interface IVideoService
{
    Task PlaceVideo(string x, string y, string height, string width, string src);
    Task PlayVideo();
    Task PauseVideo();
    Task LetFinish();
}


public class VideoService(IJSRuntime jsRuntime) : IVideoService
{

    // [JSInvokable]
    // public async Task OnVideoEnd(Task onFinishedTask)
    // {
    //     await onFinishedTask;
    // }

    private readonly IJSRuntime jsRuntime = jsRuntime;

    private DotNetObjectReference<VideoService> objRef = null!;

    public async Task Init(bool disableContextMenu = true)
    {
        objRef = DotNetObjectReference.Create(this);
        await jsRuntime.InvokeVoidAsync("video.init", objRef);
    }

    // Here goes all the video methods: 
    public async Task PlaceVideo(string x, string y, string height, string width, string src)
    {
        await jsRuntime.InvokeVoidAsync("video.placeVideo", x, y, height, width, src);

    }
    public async Task PlayVideo()
    {
        await jsRuntime.InvokeVoidAsync("video.playVideo");
    }
    public async Task PauseVideo()
    {
        await jsRuntime.InvokeVoidAsync("video.pauseVideo");
    }
    public async Task LetFinish()
    {
        await jsRuntime.InvokeAsync<object>("video.letFinish");
    }

}