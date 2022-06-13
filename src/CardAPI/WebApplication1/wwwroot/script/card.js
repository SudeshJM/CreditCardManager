// JavaScript source code

$(document).ready(function(){
    $("#AddCard").load("/views/AddCard.html");
    $("#CardList").load("/views/CardList.html");

    $(document).on('click', '#addCardButton', addCard);
    $('#AddCardError').html('');
    $('#AddCardSuccess').html('');

    function addCard(event) {       
        $('#AddCardError').html('');
        $('#AddCardSuccess').html('');

        var form = $("#addCardForm")[0];
        var isValid = form.checkValidity();
        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
            return;
        }

        form.classList.remove('was-validated');
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
                if (data.status && status !== 200) {
                    displayErrors(data);
                    return;
                }

                handleAddSuccessful(data);
            })
            .catch(error => {
                $('#AddCardError').html('<span>' + error + '</span>');
            });
    }

    function getCards() {
        fetch("/api/v1/card/cards")
            .then(response => response.json())
            .then(data => displayCards(data))
            .catch(error => console.error("Unable to get Cards.", error));
    }

    function handleAddSuccessful(data) {
        $('#AddCardSuccess').html('<span>Card is successfully added.</span>')
        addItemToTable(data);
        $('#CardName').val('');
        $('#CardNumber').val('');
        $('#CardLimit').val('');
    }

    function addItemToTable(item) {
        var html = getItemHtml(item);
        $('#CardsTable tbody').prepend(html);
    }

    function displayErrors(data) {
        var html = '<div>' + data.title + '</div>';
        if (data.detail) {
            html += '<div>' + data.detail + '</div>';
        }
        else if(data.errors) {
            html += '<div><span>' + JSON.stringify(data.errors).replace(/[\])}[{(]/g, '')  + '</span></div>';        
        }

        $('#AddCardError').html(html);
    }

    function displayCards(data) {
        var html=''
        $(data).each(function (index, item) {
            html += getItemHtml(item);
        });
        $('#CardsTable tbody').html(html);
    }

    function getItemHtml(item) {
       return '<tr><td>' + item.name + '</td><td>' + item.cardNumber + '</td><td>' + item.currency + ' ' + item.balance + '</td><td>' + item.currency + ' ' + item.limit + '</td></tr>';
    }

    getCards();
});
