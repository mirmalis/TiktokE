function authorID(element){
  return element.querySelector("video").getAttribute("authorid");
}
function channelID(){
  //document.querySelector("video").getAttribute("authorid")
  let r = /profile\/(\d+)\?/;
  let match = r.exec(
    document.querySelector("meta + [property^='al:android:url']")?.content 
    ?? document.querySelector("meta + [property^='al:ios:url']")?.content
  );
  if(match){
    return match[1];
  }
  return null;
}

fetch('https://localhost:5001/api/Channels', {
  method: 'POST', // or 'PUT'
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    "ttid": channelID(),
    "handle": document.querySelector(".share-title-container h2").innerText,
    "name": document.querySelector(".share-title-container h1").innerText
  }),
})
  .then(response => response.json())
  .then(data => {
    console.log('Success:', data);
  })
  .catch((error) => {
    console.error('Error:', error);
  });