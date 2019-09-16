class Chatbot {

    constructor() {
        //Properties
        this.id = '5d5f41c98asd8g132c';
        this.name = 'El chatbot de la gente';
        this._who = 'Bot';
        this.webService = 'http://localhost/ChatBot/Chatbot/chatbotWS.asmx/HelloWorld'
        //Create function
        this.create();

        //Add the styles to the page
        this.addStyles();
    }

    who() {
        this._who = (this._who == 'Bot' ? 'User' : 'Bot');
        return this._who;
    }

    create() {
        //Body de la página
        const body = document.getElementsByTagName("body")[0];

        //Creo todos los elementos del chatbot
        const chatbot = document.createElement('div');

        //Configuro todos los elementos.
        chatbot.classList.add("chatbot");

        //Chatbot append
        chatbot.appendChild(this.createHeader());
        chatbot.appendChild(this.createBody());
        chatbot.appendChild(this.createFooter());

        //Body append
        body.appendChild(chatbot);
        console.log('Chatbot creado');
    }

    createHeader() {
        const header = document.createElement('header');
        const name = document.createElement('h3');
        const close = this.createImage('close.png');

        //Set the name of the chatbot;
        name.innerHTML = this.name;

        //Header append
        header.appendChild(name);
        header.appendChild(close);

        return header;
    }

    createBody() {
        const body = document.createElement('div');

        body.id = 'chat';
        body.classList.add('chat');

        return body;
    }

    createFooter() {
        const footer = document.createElement('footer');
        const textarea = document.createElement('textarea');
        const send = this.createImage('send.png');

        send.onclick = this.sendOnClick;
        textarea.id = this.id + 'input';
        textarea.rows = '3';
        footer.appendChild(textarea);
        footer.appendChild(send);

        return footer;
    }

    createImage(image) {
        const div = document.createElement('div');
        const img = document.createElement('img');
        //Set the id of the div.
        div.id = image.replace('.png', '');
        div.classList.add('img');
        //Set the source of the image;
        img.src = 'http://localhost/ChatBot/Chatbot/' + image;
        //Append the image to the source
        div.appendChild(img);
        //Return the image div
        return div;
    }

    createBubble(who, text) {
        //Create all the elements.
        const name = document.createElement('div');
        const bubble = document.createElement('div');
        const chat = document.getElementById('chat');
        //Add the class of the chat.
        name.classList.add('messageName' + who);
        bubble.classList.add('message' + who);
        //Insert the text and name.
        name.innerHTML = (who == 'Bot' ? chatbot.name : 'Tu');
        bubble.innerHTML = text;
        //Adding the message to the chat body.
        chat.appendChild(name);
        chat.appendChild(bubble);
    }

    addStyles() {
        var fileref = document.createElement("link");
        var filename = 'http://localhost/ChatBot/Chatbot/chatbot.css'
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);

        if (typeof fileref != "undefined")
            document.getElementsByTagName("head")[0].appendChild(fileref)

    }

    sendOnClick() {
        const text = document.getElementById(chatbot.id + 'input');

        chatbot.createBubble(chatbot.who(), text.value);

        text.value = '';
        text.focus;
    }

    callWebService() {
        // 1. Create a new XMLHttpRequest object
        let xhr = new XMLHttpRequest();
        // 2. Configure it: GET-request for the URL /article/.../load
        xhr.open('POST', chatbot.webService)

        // 3. Send the request over the network
        xhr.send({
            text: 'Hola como estas',
            
        });

        // 4. This will be called after the response is received
        xhr.onload = function () {
            if (xhr.status != 200) { // analyze HTTP status of the response
                alert(`Error ${xhr.status}: ${xhr.statusText}`); // e.g. 404: Not Found
            } else { // show the result
                alert(`Done, got ${xhr.response.length} bytes`); // responseText is the server
            }
        };

        xhr.onprogress = function (event) {
            if (event.lengthComputable) {
                alert(`Received ${event.loaded} of ${event.total} bytes`);
            } else {
                alert(`Received ${event.loaded} bytes`); // no Content-Length
            }

        };

        xhr.onerror = function () {
            alert("Request failed");
        };
    }
}

var chatbot = new Chatbot();