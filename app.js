var express = require('express');
var model = require('./model.js');
var databaseHandler = require('./fileDb');

var app = express();

var server = app.listen(7000, function () {
    console.log('Listening on port 7000...')
});

var urlencodedParser = express.urlencoded({extended: true});
var jsonParser = express.json();

app.get('/', function(req, res){
    res.send("ok");
});

app.get('/home', function (req, res) {
    let filter = new model.Filter(req.query.page, req.query.pageSize, req.query.category, req.query.from, req.query.to);
    let filteredNotesInfo = getFilteredNotes(filter);
    res.send(JSON.stringify(filteredNotesInfo));
});


app.get('/categories', function(req, res){
   let categories = databaseHandler.getAllCategories();
   res.send(JSON.stringify(categories)) 
});


app.get('/note/:title', function(req, res){
    let title = req.params.title;
    let note = databaseHandler.getNoteByTitle(title);
    res.send(JSON.stringify(note));
});

app.post('/note', jsonParser, function (req, res) {
    let note = model.Note.createFromJson(req.body);
    if(databaseHandler.titleExists(note.title)){
        res.status(400);
        res.send(JSON.stringify("Title already exists."))
    }else {
        databaseHandler.addNote(note);
        res.send(JSON.stringify('Note added'));
    }
});

app.put('/note', jsonParser, function(req, res){
   let note = model.Note.createFromJson(req.body);
   databaseHandler.deleteNote(note.title);
   databaseHandler.addNote(note);
   res.send(JSON.stringify('Note edited'));
});


app.delete('/note/:title', function (req, res) {
    const title = req.params.title;
    databaseHandler.deleteNote(title);
    res.send(JSON.stringify('Note deleted'))
});


function getFilteredNotes(filter) {
    let notes = databaseHandler.getAllNotes();
    return filterNotes(notes, filter);
}

function filterNotes(notes, filter) {
    let filteredNotes = notes;

    if (filter.category !== "") {
        filteredNotes = filteredNotes.filter(e => e.categories.find(c => c === filter.category) !== undefined);
    }

    if (filter.startDate !== "" ) {
        filteredNotes = filteredNotes.filter(e => e.date.getTime() >= filter.startDate.getTime());
    }

    if (filter.endDate !== "") {
        filteredNotes = filteredNotes.filter(e => e.date.getTime() <= filter.endDate.getTime());
    }
    let totalNotes = filteredNotes.length;

    if (!isNaN(filter.page) && !isNaN(filter.pageSize)) {
        const start = (filter.page - 1) * filter.pageSize;
        const end = start + filter.pageSize < totalNotes ? start + filter.pageSize : totalNotes; 
        filteredNotes = filteredNotes.slice(start, end);
    }

    return {notes: filteredNotes, total: totalNotes};
}


