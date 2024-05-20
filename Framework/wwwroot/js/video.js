window.video = {
    objRef: null,
    init: init,
    video: null, // 
    // Here go all the functions to manage the videos
    placeVideo: function(x, y, height, width, src){ // Function to place a video on the screen
        this.vidTag.style.top = y+"px";
        this.vidTag.style.right = x+"px";
        this.vidTag.style.width = width+"px";
        this.vidTag.style.height = height+"px";
        this.srcTag.src = src;
        this.vidTag.load()
    },
    playVideo: function(){
        this.vidTag.play();
    },
    pauseVideo: function(){
        this.vidTag.pause();
    },
    letFinish: function(){
        return new Promise((resolve, reject) => {
            console.log("letFinish called on js");
            this.vidTag.onended = () => {
                // When the video is finished, change back to VideoService.cs
                console.log("onended");
                resolve();
            }
        });
    }
    
}


function init(objRef){
    window.video.objRef = objRef;
    // Make BalzoredVideo/src tag and add them to the Document
    window.video.vidTag = document.createElement('video'); 
    window.video.vidTag.id = "vidtag";
    window.video.vidTag.controls = true; // Not necessary, only for tests
    window.video.vidTag.style.height = "0"; // Hide the video-tag => 0x0px
    window.video.vidTag.style.position = "fixed";
    window.video.srcTag = document.createElement('source');
    window.video.srcTag.type = "video/mp4";
    window.video.vidTag.appendChild(window.video.srcTag);
    document.body.appendChild(window.video.vidTag);

}