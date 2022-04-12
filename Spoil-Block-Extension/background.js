console.log("hi from background");

let hasSetYouTubeListener = false;

chrome.tabs.onUpdated.addListener(tab => {
    chrome.tabs.get(tab, current_tab_info => {
        if (/^https:\/\/www\.youtube/.test(current_tab_info.url) && current_tab_info.status === "complete" && hasSetListener === false) {
            console.log(current_tab_info);
            hasSetYouTubeListener = true;
            chrome.scripting.executeScript({
                target: {tabId: current_tab_info.id},
                files: ['foreground.js']
            });
        }
        else if (current_tab_info.url.includes('spoilblock.azurewebsites.net/Watchlist'))
        {
            console.log('hello watchlist');
            chrome.scripting.executeScript({
                target: {tabId: current_tab_info.id},
                files: ['watchlist-update.js']
            });
        }
    });
});


// chrome.tabs.onActivated.addListener(tab => {
//     chrome.tabs.get(tab.tabId, current_tab_info => {
//         if (/^https:\/\/www\.youtube/.test(current_tab_info)) {
//             chrome.tabs.executeScript(null, {file: './foreground.js'}, () => console.log('i injected'));
//         }
//     });
// });

let user_signed_in = false;
const CLIENT_ID = encodeURIComponent('1071004128569-bhrhldj4hbus2hlkeokk1g35bsqqsd0i.apps.googleusercontent.com');
const RESPONSE_TYPE = encodeURIComponent('id_token');
const REDIRECT_URI = encodeURIComponent('https://fgmfibnmhmabgciknfebajbdbcjkdoop.chromiumapp.org');
const STATE = encodeURIComponent('nefnfsd');
const SCOPE = encodeURIComponent('openid');
const PROMPT = encodeURIComponent('consent');

function create_oauth2_url() {
    let nonce = encodeURIComponent(Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15));

    let url =
    `https://accounts.google.com/o/oauth2/v2/auth
?client_id=${CLIENT_ID}
&response_type=${RESPONSE_TYPE}
&redirect_uri=${REDIRECT_URI}
&state=${STATE}
&scope=${SCOPE}
&prompt=${PROMPT}
&nonce=${nonce}
    `;

    console.log(url);

    return url;
}

function is_user_signed_in() {
    return user_signed_in;
}

chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
    if (request.message === 'login') {
        if (is_user_signed_in()) {
            console.log("User is already signed in.");
        } else {
            //create_oauth2_url();
        }
    } else if (request.message === 'logout') {

    } else if (request.message === 'isUserSignedIn') {

    }
});