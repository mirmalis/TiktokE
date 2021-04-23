
// document.querySelector("[alt=Google]").classList.add('spinspinspin');
// document.querySelector("[title=TikTok]").classList.add('spinspinspin');
var tiktokIcon = document.querySelector("[title=TikTok]");
tiktokIcon.classList.add("spinspinspin");
setTimeout(() => tiktokIcon.classList.remove("spinspinspin"), 2000);
tiktokIcon.addEventListener('contextmenu', (e) => console.log(e));
// const first = document.createElement('button');
// first.innerText = "SET DATA";
// first.id = "first";
// document.querySelector("body").insertBefore(first, document.querySelector("body"));

// const second = document.createElement('button');
// second.innerText = "SHOUTOUT TO BACKEND";
// second.id = "second";

// document.querySelector('body').appendChild(first);
// document.querySelector('body').appendChild(second);

// first.addEventListener('click', () => {
//   // chrome.storage.sync
//   // chrome.storage.local
//   chrome.storage.local.set({"password": "123"});
//   console.log("I SET DATA");
// });
// second.addEventListener("click", () => {
//   chrome.runtime.sendMessage({message: 'yo check the storage'});
//   console.log("I sent the message");
// });

// chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
//   console.log(request.message);
// });