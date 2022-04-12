document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('sortTable');
    const headers = table.querySelectorAll('th');
    const tableBody = table.querySelector('tbody');
    const rows = tableBody.querySelectorAll('tr');


    const directions = Array.from(headers).map(function (header) {
        return '';
    });


    const transform = function (index, content) {

        const type = headers[index].getAttribute('data-type');
        switch (type) {
            case 'number':
                return parseFloat(content);
            case 'string':
            default:
                return content;
        }
    };
    const sortList = function (index) {

        const direction = directions[index] || 'asc';


        const multiplier = direction === 'asc' ? 1 : -1;

        const newRows = Array.from(rows);

        newRows.sort(function (rowA, rowB) {
            const cellA = rowA.querySelectorAll('td')[index].innerHTML;
            const cellB = rowB.querySelectorAll('td')[index].innerHTML;

            const a = transform(index, cellA);
            const b = transform(index, cellB);

            switch (true) {
                case a > b:
                    return 1 * multiplier;
                case a < b:
                    return -1 * multiplier;
                case a === b:
                    return 0;
            }
        });


        [].forEach.call(rows, function (row) {
            tableBody.removeChild(row);
        });


        directions[index] = direction === 'asc' ? 'desc' : 'asc';


        newRows.forEach(function (newRow) {
            tableBody.appendChild(newRow);
        });
    };

    [].forEach.call(headers, function (header, index) {
        header.addEventListener('click', function () {
            sortList(index);
        });
    });
});


$(document).ready(function () {
    $(document).on("click", "button[name$='deleteButton']", function () {
        if (confirm(`Really delete ${$(event.target).parent().parent().find('.imageandname').text()} from your watchlist?`)) {
            $.ajax({
                url: '/Add/DeleteShowFromWatchlist',
                data: { id: `${$(event.target).attr("id")}` },
                method: "POST",
                success: removeShowFromDisplay
            });
        }
    });
});


function removeShowFromDisplay(data) {
    window.location.replace('/Watchlist');
}