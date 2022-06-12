// JavaScript source code

$(document).ready(function(){
    $("#AddCard").load("/views/AddCard.html");
    $("#CardList").load("/views/CardList.html");

    $(document).on('click', '#addCardButton', addCard);
    $('#add-card-error').html('');

    function addCard(event) {       
        var form = $("#addCardForm")[0];
        var isValid = form.checkValidity();
        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
            return;
        }

        form.classList.remove('was-validated');
        $('#add-card-error').html('');

        const cardName = $('#CardName').val();
        const cardNumber = $('#CardNumber').val();
        const cardLimit = $('#CardLimit').val();

        const data = {
            name: cardName,
            cardNumber: cardNumber,
            limit: parseInt(cardLimit) || 0
        };
        fetch("/api/v1/card/addcard", {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then(data => {
                if (data.errors) {
                    displayErrors(data);
                    return;
                }
                addItemToTable(data);
                $('#CardName').val('');
                $('#CardNumber').val('');
                $('#CardLimit').val('');
            })
            .catch(error => {
                $('#add-card-error').html('<span>' + error + '</span>');
            });
    }

    function getCards() {
        fetch("/api/v1/card/cards")
            .then(response => response.json())
            .then(data => displayCards(data))
            .catch(error => console.error("Unable to get Cards.", error));
    }

    function displayErrors(data) {
        var html = data.title
        $(data.errors).each(function (index, item) {
            html += '<div>' + item + '</div>';
        });
        $('#add-card-error').html(html);
    }

    function addItemToTable(item) {
        var html = '<tr><td>' + item.name + '</td><td>' + item.cardNumber + '</td><td>' + item.balance + '</td><td>' + item.limit + '</td></tr>';
        $('#CardsTable tbody').prepend(html);
    }

    function displayCards(data) {
        var html=''
        $(data).each(function (index, item) {
            html += '<tr><td>' + item.name + '</td><td>' + item.cardNumber + '</td><td>' + item.balance + '</td><td>' + item.limit + '</td></tr>';
        });
        $('#CardsTable tbody').html(html);
    }

    getCards();
});
