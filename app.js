var express = require('express');
var fs = require('fs');
// var model = require('./model.js');
var app = express();

// start the server
var server = app.listen(8000, function () {
    console.log('Listening on port 8000...')
});

var urlencodedParser = express.urlencoded({extended: true});
var jsonParser = express.json();

// app.use(bodyParser.json());
// app.use(express.urlencoded({ extended: true }));

app.get('/home', function (req, res) {
    let filter = new Filter(req.query.page, req.query.pageSize, req.query.category, req.query.startDate, req.query.endDate);
    let filteredNotes = getFilteredNotes(filter);
    res.send(JSON.stringify(filteredNotes))
});

app.post('/note', jsonParser, function (req, res) {
    let note = Note.createFromJson(req.body);
    // let note = new Note(req.body.title, req.body.description, req.body.isMarkdownFile, req.body.date, req.body.categories);
    addNote(note);
    res.send('Wysłano');
});

app.delete('/note', function (req, res) {
    const title = req.query.noteTitle;
    deleteFile(title);
    res.send('Usunieto')
});

app.post('/new', urlencodedParser, function (req, res) {
    console.log(req.body.name)
});

app.post('/home', function (req, res) {
    console.log(JSON.stringify(req.body));
    console.log(req.body.name);
    res.send();
});

class Filter {
    constructor(page, pageSize, category, startDate, endDate) {
        this.page = parseInt(page);
        this.pageSize = parseInt(pageSize);
        this.category = category;
        this.startDate = startDate !== undefined ? new Date(startDate) : startDate;
        this.endDate = endDate !== undefined ? new Date(endDate) : endDate;
    }
}

class Note {
    constructor(title, description, isMarkdownFile, date, categories) {
        this.title = title;
        this.description = description;
        this.isMarkdownFile = isMarkdownFile;
        this.date = new Date(date);
        this.categories = categories;
    }

    //napisac konstruktor dla jsona
    static createFromJson = function (jsonObject) {
        return new Note(jsonObject.title, jsonObject.description, jsonObject.isMarkdownFile, jsonObject.date, jsonObject.categories);
    };
}

function getFilteredNotes(filter) {
    let notes = getAllNotes();
    notes = filterNotes(notes, filter);
    return notes;
}

function getAllNotes() {
    let notes = [];
    let fileNames = fs.readdirSync('./db');
    for (let i = 0; i < fileNames.length; i++) {
        let filename = fileNames[i];
        let rawData = fs.readFileSync('./db/' + filename);
        let jsonObject = JSON.parse(rawData.toString());
        let note = Note.createFromJson(jsonObject);
        notes.push(note);
    }
    return notes;
}


function filterNotes(notes, filter) {
    let filteredNotes = notes;

    if (filter.category !== undefined) {
        filteredNotes = filteredNotes.filter(e => e.categories.find(c => c === filter.category) !== undefined);
    }

    if (filter.startDate !== undefined) {
        filteredNotes = filteredNotes.filter(e => e.date.getTime() >= filter.startDate.getTime());
    }

    if (filter.endDate !== undefined) {
        filteredNotes = filteredNotes.filter(e => e.date.getTime() <= filter.endDate.getTime());
    }

    if (!isNaN(filter.page) && !isNaN(filter.pageSize)) {
        const start = filter.page * filter.pageSize;
        filteredNotes = filteredNotes.slice(start, start + filter.pageSize);
    }

    return filteredNotes;
}


function deleteFile(noteTitle) {
    let notes = getAllNotes();
    notes.forEach(function (note) {
        if (note.title === noteTitle) {
            let path = getFilePath(note);
            fs.unlinkSync(path);
        }
    });
}

function getFilePath(note) {
    let path = './db/' + note.title;
    if (note.isMarkdownFile) {
        path += '.md';
    } else {
        path += '.txt';
    }
    return path;
}

function addNote(note) {
    let path = getFilePath(note);
    fs.writeFileSync(path, JSON.stringify(note));
}

