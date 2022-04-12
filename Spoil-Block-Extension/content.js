function blurTitle (element) {
    element.style.color = 'transparent';
    element.style.textShadow = '0 0 5px rgba(0,0,0,0.5)';
}

function blurThumbnail (element) {
    element.style.filter = 'blur(10px)';
}

function checkSpoiler(element) {
    let title = element.querySelector("#video-title");
    let thumbnail = element.querySelector("#img");

    let titleText = title.innerHTML.toString().toLowerCase();
    let isSpoiler = false;

    if (titleText.includes('spoiler') && !titleText.includes('no spoiler')) { isSpoiler = true; }
    if (titleText.includes('spoiler-free') || titleText.includes('spoiler free') || titleText.includes('non-spoiler')) {isSpoiler = false; }

    if(isSpoiler) {
        blurTitle(title);
        blurThumbnail(thumbnail);
    }
}

function removeListeners() {
    
    
}

function addListenerContents(element) {
    document.getElementById(element).removeEventListener("DOMNodeInserted", function (event) {});

    document.getElementById(element).addEventListener("DOMNodeInserted", function (event) {
        if (el) {
            let els = document.querySelectorAll('[id=dismissible]');
            els.forEach(
                function(currentValue) {
                    checkSpoiler(currentValue);
                }
            );
        } else {
            console.log("Not yet loaded");
        }
    });
    
    //------------------------------------------------
    let el = document.getElementById(element);
}

function addListenerItems(element) {
    let related = document.getElementById(element).querySelector("#items");

    let relatedDiv = document.getElementById("related").querySelector('#items');
    relatedDiv.removeEventListener("DOMNodeInserted", function (event) {} );

    console.log(document.getElementById(element).children[1]);
    console.log(document.getElementById(element).children[1].querySelector(":scope > #items"));

    related.addEventListener("DOMNodeInserted", function (event) {
        let els = related.querySelectorAll('#dismissible');
        els.forEach(
            function(currentValue) {
                checkSpoiler(currentValue);
                //console.log(currentValue);
            }
        );
    });
}

async function IsLoaded() {
    console.log(location.href);
    console.log("adding listener");
    if (location.href.includes('youtube.com/watch')) {
        console.log("adding listener to related div");
        addListenerItems("related");
    } else {
        addListenerContents("contents");
    }
}

function pageWait() {
return new Promise(resolve => {
    setTimeout(() => {
    IsLoaded();
    }, 200);
});
}
  
async function asyncCall() {
    console.log('calling');
    const result = await pageWait();

    console.log(result);
}

async function checkPageLoad() {
    setTimeout(() => {
        let title = document.getElementById("video-title");
        if (title) {
            if (title.innerHTML === null) {
                console.log("Tried to check contents... wasn't loaded. Running again.");
                setTimeout(() => { checkPageLoad() }, 50)
                //checkPageLoad();
            } else {
                console.log("Tried to check contents... IT'S LOADED! Breaking out of checkPageLoad()");
                IsLoaded();
            }
        } else {
            console.log("title was null. Running again.");
            setTimeout(() => { checkPageLoad() }, 50);
        }
    }, 0);
}

let lastUrl = location.href; 
new MutationObserver(() => {
  const url = location.href;
  if (url !== lastUrl) {
    lastUrl = url;
    onUrlChange();
  }
}).observe(document, {subtree: true, childList: true});

function onUrlChange() {
    location.reload();
}

checkPageLoad();

