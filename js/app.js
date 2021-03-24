// import Something from "./another-js-file.js";

class App {
    constructor() {
        // setup         
    }

    go() {
        // retrieve and display the list of books
        this.getBooks();
    }

    //binds the api result into html
    renderBooks(books) {      

        let html = '<ol>';      
        books.forEach(book => {
            let htmlSegment = `<li>                                
                                <a href='#' onClick='javascript:getWordCount(${book.Id},"${book.BookName}");' id='${book.Id}'>${book.BookName}</a>
                                </li>`;

            html += htmlSegment;
        });

        let container = document.querySelector('.container');
        container.innerHTML = html + '</ul>';
    }
    //call api to get list of books
    getBooks() {
        let url = '/api/books';
        let container = document.querySelector('.container');
        container.innerHTML = '<div>Loading...</div>';
        console.log(url);
        try {
            let res = fetch(url)
                .then(response => {
                    return response.json()
                })
                .then(data => {
                    let books = data;
                    this.renderBooks(books);
                })
        } catch (error) {
            console.log(error);
        }
    }
}
new App().getBooks();
