document.querySelector('#goto-watchlist-btn').addEventListener('click', function () {
    chrome.tabs.create({
        url: 'https://spoilblock.azurewebsites.net/Watchlist'
    });
});
