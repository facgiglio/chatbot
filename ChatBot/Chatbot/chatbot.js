class Chatbot {

    constructor(chatbotId, chatbotName) {
        //Properties
        this.id = chatbotId;
        this.name = chatbotName;
        this.who = 'User';
        this.webService = 'http://localhost/ChatBot/Chatbot/chatbotWS.asmx/HelloWorld'

        //Create function
        this.create();

        //Add the styles to the page
        this.addStyles();

        //Properties base on the construction of the chatbot.
        this.body = document.getElementById(this.id + 'body');
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

        body.id = this.id + 'body';
        body.classList.add('chat');

        return body;
    }

    createFooter() {
        const footer = document.createElement('footer');
        const textarea = document.createElement('textarea');
        const send = this.createImage('send.png');

        send.onclick = this.sendOnClick;
        send.id = this.id + 'send';
        textarea.id = this.id + 'input';

        textarea.rows = '3';

        // Execute a function when the user releases a key on the keyboard
        textarea.addEventListener("keyup", function (event) {
            // Number 13 is the "Enter" key on the keyboard
            if (event.keyCode === 13) {
                // Cancel the default action, if needed
                event.preventDefault();
                // Trigger the button element with a click
                document.getElementById(chatbot.id + 'send').click();
            }
        });

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

    createBubble(text) {
        //Create all the elements.
        const name = document.createElement('div');
        const bubble = document.createElement('div');

        //Add the class of the chat.
        name.classList.add('messageName' + chatbot.who);
        bubble.classList.add('message' + chatbot.who);

        //Insert the text and name.
        name.innerHTML = (chatbot.who == 'Bot' ? chatbot.name : 'Tu');
        bubble.innerHTML = text;

        //Adding the message to the chat body.
        chatbot.body.appendChild(name);
        chatbot.body.appendChild(bubble);

        //After create the bubble, scroll down the body.
        chatbot.body.scrollTop = chatbot.body.scrollHeight;
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

        chatbot.who = 'User';
        chatbot.createBubble(text.value);
        chatbot.webServiceCall(text.value);

        text.value = '';
        text.focus;
    }

    webServiceCall(message) {
        //Create the soap Header
        var soap = chatbot.createSoapHeader(message);
        //create the web service object and make the call
        var webServiceCall = new WebSvc();
        webServiceCall.CallWebService("/ChatBot/Chatbot/chatbotws.asmx", soap, chatbot.callComplete);
    }

    createSoapHeader(message) {
        var soap = '' +
            '<?xml version="1.0" encoding="utf-8"?>' +
            '  <soap12:Envelope ' +
            '   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" ' +
            '   xmlns:xsd="http://www.w3.org/2001/XMLSchema" ' +
            '   xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">' +
            '    <soap12:Body>' +
            '      <Chat xmlns="http://localhost/ChatBot/">' +
            '        <input>' + message + '</input>' +
            '        <hashKey>' + chatbot.id + '</hashKey>' +
            '        <hostName>' + window.location.href + '</hostName>' +
            '      </Chat>' +
            '    </soap12:Body>' +
            '  </soap12:Envelope>';


        return soap;
    }

    callComplete(result, data) {
        if (result) {
            chatbot.who = 'Bot';
            chatbot.createBubble(chatbot.getTagValue(data, 'ChatResult'));
        }
        else {
            alert("Error occurred calling web service.");
        }
    }

    getTagValue(inputStr, tagName) {
        var stag = "<" + tagName + ">";
        var etag = "</" + tagName + ">";

        var startPos = inputStr.indexOf(stag, 0);
        if (startPos >= 0) {
            var endPos = inputStr.indexOf(etag, startPos);
            if (endPos > startPos) {
                startPos = startPos + stag.length;
                return inputStr.substring(startPos, endPos);
            }
        }

        return "";
    }
}

function WebSvc()   // Class Signature
{
    // Encapsulates the elements of a XMLHttpRequest to an ASP .Net Web Service
    WebSvc.prototype.CallWebService = function (url, soapXml, callback) {

        // Calls web service with XMLHttpRequest object. Utilizes a SOAP envelope for
        // transport to and from Server. This is an asynchronous call.
        // PARAM url - Fully qualified url to web service .asmx file
        // PARAM sopXml - String containing SOAP envelope with request
        // PARAM callback - Callback with signature callback(result,data) when call returns.
        var xmlDoc = null;

        if (window.XMLHttpRequest) {
            xmlDoc = new XMLHttpRequest(); //Newer browsers
        }
        else if (window.ActiveXObject) //IE 5, 6
        {
            xmlDoc = new ActiveXObject("Microsoft.XMLHTTP");
        }

        if (xmlDoc) {
            //callback for readystate when it returns
            var self = this;
            xmlDoc.onreadystatechange = function () { self.StateChange(xmlDoc, callback); };

            //set up the soap xml web service call
            xmlDoc.open("POST", url, true);
            xmlDoc.setRequestHeader("Content-Type", "application/soap+xml");
            xmlDoc.setRequestHeader("Access-Control-Allow-Origin", "*");
            //xmlDoc.setRequestHeader("Content-Length", soapXml.length);
            xmlDoc.send(soapXml);
        }
        else {
            if (callback) {
                callback(false, "unable to create XMLHttpRequest object");
            }
        }
    };

    WebSvc.prototype.StateChange = function (xmlDoc, callback) {
        // Callback supplied for XMLHttpRequest Object to monitor state and retrieve data.
        // PARAM xmlDoc - XMLHttpRequest Object we're watching
        // PARAM callback - Callback function for returning data, signature CallBack(result,data)

        if (xmlDoc.readyState === 4) {
            var text = "";

            if (xmlDoc.status === 200) {
                text = xmlDoc.responseText;
            }

            // Perform callback with data if callback function signature was provided, 
            if (callback !== null) {
                callback(xmlDoc.status === 200, text);
            }
        }
    };
}