
window.element_currently_in_focus = undefined;
document.onkeydown = (ev) => {
  function POSTObject(element){
    // let element = document.querySelector(".lazyload-wrapper").nextElementSibling;
    let r = {
      "ttid": /video\/(\d*)/.exec(element.querySelector(".item-video-container a").href)[1],
      "channel": {
        "name": /([^<]*)+/.exec(element.querySelector(".author-nickname").innerHTML)[1],
        "ttid": element.querySelector("video")?.getAttribute("authorId"),
        "handle": /@([^?]*)/.exec(element.querySelector(".tt-author-info a").href)[1],
        "verified": element.querySelector(".verified") !== null
      },
      "audio": {
        "name": /music\/(.*)-(\d+)/.exec(element.querySelector(".tt-video-music a").href)[1],
        "ttid": /music\/(.*)-(\d+)/.exec(element.querySelector(".tt-video-music a").href)[2]
      },
      "src": element.querySelector("video")?.getAttribute("src"),
      "description": element.querySelector(".tt-video-meta-caption")?.innerText
    };
    //console.log(r);
    return r;
  }
  function zoomToElement(element){
    if (element == null){
      console.log('Error: no element to zoom to.');
      return;
    }
    window.element_currently_in_focus = element;
    var y = element.getBoundingClientRect().top + window.pageYOffset + -60;
    
    setTimeout(() => {
      window.scrollTo({top: y, behavior: 'smooth'});
    }, 5);
    setTimeout(() => {
      fetch('https://localhost:5101/api/Videos', {
        method: 'POST', // or 'PUT'
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(POSTObject(element)),
      })
      .then(response => response.json())
      .then(data => {
        // console.log('Success:', data);
      })
      .catch((error) => {
        console.error('Error:', error);
      });
    }, 250);
  }
  function showNext(shift){
    function getElementByShift(shift){
      if (window.element_currently_in_focus == null){
        return document.querySelector(".lazyload-wrapper");
      } else {
        if(shift > 0){
          return window.element_currently_in_focus.nextElementSibling;
        } else if(shift < 0){
          return window.element_currently_in_focus.previousElementSibling;
        } else {
          return document.querySelector(".lazyload-wrapper");
        }
      }
    }
    zoomToElement(getElementByShift(shift));
  }
  switch (ev.key) {
    case "ArrowRight":
      if (document.querySelector(".arrow-right") != null){
        document.querySelector(".arrow-right").click();
      } else {
        showNext(1);
      }
      break;
    case "ArrowLeft":
      if (document.querySelector(".arrow-right") != null){
        document.querySelector(".arrow-right").click();
      } else {
        showNext(-1);
      }
      break;
    case "ArrowDown":
      let videoLink = window.element_currently_in_focus.querySelector(".item-video-container").querySelector("a").href;
      var regexMatch = /.*@([^/]*)\/video\/(\d*)/.exec(videoLink);
      let channelHandle = regexMatch[1];
      let videoID = regexMatch[2];
      let channelID = window.element_currently_in_focus.querySelector("video").getAttribute("authorid");
      chrome.runtime.sendMessage({
        purpose: 'download', 
        data: { 
          src: window.element_currently_in_focus.querySelector("video").src, 
          link: videoLink,
          channelHandle,
          videoID,
          "lnks":[
            {
              url: URL.createObjectURL(new Blob([`C:/windows/explorer.exe .\\..\\..\\channels\\${channelHandle}`], {type: "text/plain"})),
              filename: `TikTokE/ChannelIDs/${channelID}/${new Date().toLocaleDateString("lt")}_${channelHandle}.bat.txt`
            },
            {
              url: URL.createObjectURL(new Blob([channelID], {type: "text/plain"})),
              filename: `TikTokE/Channels/${channelHandle}/ids/${new Date().toLocaleDateString("lt")}.txt`
            }
          ]
        }
      });
      break;
    case "Home":
      window.element_currently_in_focus = zoomToElement(document.querySelector(".lazyload-wrapper"));
      break;
    case "End":
      window.element_currently_in_focus = zoomToElement(document.querySelectorAll(".lazyload-wrapper").last);
      break;
    default:
      console.log(ev.key);
      break;
  }
};