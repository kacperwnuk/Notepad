var express = require('express');
var model = require('./model.js');
var databaseHandler = require('./fileDb');

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
    let filter = new model.Filter(req.query.page, req.query.pageSize, req.query.category, req.query.startDate, req.query.endDate);
    let filteredNotes = getFilteredNotes(filter);
    res.send(JSON.stringify(filteredNotes))
});

app.post('/note', jsonParser, function (req, res) {
    let note = model.Note.createFromJson(req.body);
    // let note = new Note(req.body.title, req.body.description, req.body.isMarkdownFile, req.body.date, req.body.categories);
    databaseHandler.addNote(note);
    res.send('Wysłano');
});

app.delete('/note', function (req, res) {
    const title = req.query.noteTitle;
    databaseHandler.deleteNote(title);
    res.send('Usunieto')
});


function getFilteredNotes(filter) {
    let notes = databaseHandler.getAllNotes();
    notes = filterNotes(notes, filter);
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


