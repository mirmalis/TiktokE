////https://www.youtube.com/watch?v=-dhMbVEreII
console.log('background.js');
let isLoading = false;
let lastState = null;
const TikTokPages = { "notTikTok": 0, "foryou": 1, "following": 2, "channel": 3, "video": 4, "music": 5 };
function TikTokPage(url) {
  // TODO:
  // video page which is actually foryou page when scrolling.
  // https://www.tiktok.com/@thegirlz.roblox/video/6952436049712827649?lang=en&is_copy_url=1&is_from_webapp=v1
  if (!/www\.tiktok\.com/.test(url))
    return TikTokPages.notTikTok;
  if (/^https:\/\/www\.tiktok\.com\/following/.test(url))
    return TikTokPages.following;
  if (/^https:\/\/www\.tiktok\.com\/@.*\/video/.test(url))
    return TikTokPages.video;
  if (/^https\/\/www.tiktok.com\/music/.test(url)) 
    return TikTokPages.music;
  if (/^https:\/\/www\.tiktok\.com\/@/.test(url)) 
    return TikTokPages.channel;
  return TikTokPages.foryou;
}
chrome.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
  console.log('onUpdated', changeInfo);
  if (! /^https:\/\/www\.tiktok\.com\/.*/.test(tab.url))
    return;
  switch (changeInfo.status) {
    case "loading":
      console.log("Loading@TikTok", changeInfo);
      if (changeInfo.url) {
        console.log("Loading ", changeInfo.url);
        isLoading = true;
        chrome.scripting.insertCSS({target: { tabId: tabId },files: ['./mystyles.css']});
        chrome.scripting.executeScript({target: { tabId: tabId },files: ['./js/injections/typeMutations.js']});
        chrome.scripting.executeScript({target: { tabId: tabId },files: ['./js/injections/injectNavigation.js']});
        
        let tiktokState = TikTokPage(tab.url);
        if (lastState != tiktokState) {
          lastState = tiktokState;
          switch (tiktokState) {
            case TikTokPages.following:
              chrome.scripting.executeScript(
                {
                  target: { tabId: tabId },
                  files: ['./js/tiktokpages/following.js']
                }
              );
              break;
            case TikTokPages.video:
              chrome.scripting.executeScript(
                {
                  target: { tabId: tabId },
                  files: ['./js/tiktokpages/video.js']
                }
              );
              break;
            case TikTokPages.music:
              chrome.scripting.executeScript({
                target: { tabId: tabId },
                files: ['./js/tiktokpages/music.js']
              });
              break;
            case TikTokPages.channel:
              chrome.scripting.executeScript(
                {
                  target: { tabId: tabId },
                  files: ['./js/tiktokpages/channel.js']
                }
              );
              break;
            case TikTokPages.foryou:
              chrome.scripting.executeScript(
                {
                  target: { tabId: tabId },
                  files: ['./js/tiktokpages/foryou.js']
                }
              );
              break;
            default:
              console.log(`Injection into ${TikTokPage(tab.url)} page not supported.`);
              break;
          }
        }
      } else {
        console.log("no url", changeInfo, tab.url);
      }
      break;
    case "complete":
      if (!isLoading)
        return;
      isLoading = false;
      chrome.scripting.executeScript({
        target: { tabId },
        files: ['./js/tiktokpages/all.js']
      });
      break;
    default:
      if ( !changeInfo.favIconUrl
        && (changeInfo.audible === undefined)
        && !changeInfo.title
      ) {
        console.log('onUpdated@TikTok', changeInfo);
      }
      break;
  }
});
chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  //request.purpose = "download"
  //request.data.src = "https://...";
  //request.data.link = "https://www.tiktok.com/@danshaba/video/6932204775731694849?lang=en&is_copy_url=1&is_from_webapp=v1";
  //request.data.channelID = "123123123123123";
  
  if (request.purpose === 'download') {
    let channelHandle = request.data.channelHandle;
    let videoID = request.data.videoID;
    let x = {
      url: request.data.src,
      filename: `TikTokE/Channels/${channelHandle}/${videoID}.mp4`, 
      conflictAction: "overwrite"
    };
    console.log('Downloading', x);
    chrome.downloads.download(x);
    // Shortcuts

    request.data.lnks.forEach(item => {
      console.log('Downloading', item);
      chrome.downloads.download({ ...item, conflictAction: "overwrite" });
    });
  }
});