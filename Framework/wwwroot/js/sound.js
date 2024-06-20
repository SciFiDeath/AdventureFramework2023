window.sound = {
    objRef: null,
    init: init,
    sound: null,
    music: null,
    currentLoop: null,
    currentVolume: 1,
    playSound: function (path) { // Function to play a sound via path
        this.sfx.src = path; 
        this.sfx.play();
    },
    playMusic: function (path) { // Function to play backgroundmusic via path
        this.music.src = path;
        var that = this; // Save a reference to 'this'
        loopify(this.music.src, function(err,loop) {
            // If something went wrong, `err` is supplied
            if (err) {
                return console.err(err);
            }
            // If there's a current loop, stop it
            if (that.currentLoop) {
                that.currentLoop.stop();
            }
            // Play the loop, stop it with loop.stop()
            loop.play();
            that.currentLoop = loop; // Set the current loop to the new loop
        });
    },
    stopMusic: function(){
        if (this.currentLoop) {
            this.currentLoop.stop();
        }
    },
    updateVolume: function(volume){
      this.currentVolume = volume;
      this.music.volume = volume;
      this.sfx.volume = volume; 
      if (this.currentLoop) {
          this.currentLoop.setVolume(volume);
      }
    }
    
};


function init(objRef) {
    window.sound.objRef = objRef;
    // create two audio tags, sfx for audio, and music for the background track
    window.sound.sfx = document.createElement('audio');
    window.sound.sfx.id = 'audio_sfx';
    window.sound.music = document.createElement('audio');
    window.sound.music.id = 'audio_music';
    window.sound.music.src = "";
    document.body.appendChild(window.sound.sfx);
    document.body.appendChild(window.sound.music);
}


// Loopify function to play background music
(function() {
  function loopify(uri, cb) {
      var context = new (window.AudioContext || window.webkitAudioContext)(),
          gainNode = context.createGain(),
          request = new XMLHttpRequest();

      request.responseType = "arraybuffer";
      request.open("GET", uri, true);

      gainNode.gain.value = 1; // Set initial volume

      request.onerror = function() {
          cb(new Error("Couldn't load audio from " + uri));
      };

      request.onload = function() {
          context.decodeAudioData(request.response, success, function(err) {
              cb(new Error("Couldn't decode audio from " + uri));
          });
      };

      request.send();

      function success(buffer) {
          var source;

          function play() {
              stop();
              source = context.createBufferSource();
              source.connect(gainNode).connect(context.destination);
              source.buffer = buffer;
              source.loop = true;
              source.start(0);
          }

          function stop() {
              if (source) {
                  source.stop();
                  source = null;
              }
          }

          function setVolume(volume) {
              gainNode.gain.value = volume;
          }

          cb(null, {
              play: play,
              stop: stop,
              setVolume: setVolume
          });
      }
  }

  loopify.version = "0.1";

  if (typeof define === "function" && define.amd) {
      define(function() { return loopify; });
  } else if (typeof module === "object" && module.exports) {
      module.exports = loopify;
  } else {
      this.loopify = loopify; 
  }
})();


