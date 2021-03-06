var fs = require('fs');
var model = require('./model');

function deleteFile(noteTitle) {
    let notes = getAllNotes();
    notes.forEach(function (note) {
        if (note.title === noteTitle) {
            let path = getFilePath(note);
            fs.unlinkSync(path);
        }
    });
}

function getAllNotes() {
    let notes = [];
    let fileNames = fs.readdirSync('./db');
    for (let i = 0; i < fileNames.length; i++) {
        let filename = fileNames[i];
        let rawData = fs.readFileSync('./db/' + filename);
        let jsonObject = JSON.parse(rawData.toString());
        let note = model.Note.createFromJson(jsonObject);
        notes.push(note);
    }
    return notes;
}

function addNote(note) {
    let path = getFilePath(note);
    fs.writeFileSync(path, JSON.stringify(note));
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

function getNoteByTitle(title) {
    let notes = getAllNotes();
    return notes.find(n => n.title === title);
}


function getAllCategories() {
    let notes = getAllNotes();
    let categories = new Set();
    let note;
    for (note of notes) {
        let category;
        for (category of note.categories) {
            categories.add(category)
        }
    }
    return {categories: Array.from(categories)};
}

function titleExists(title) {
    let notes = getAllNotes();
    return notes.find(n => n.title === title) !== undefined
}


module.exports.getAllNotes = getAllNotes;
module.exports.addNote = addNote;
module.exports.deleteNote = deleteFile;
module.exports.getNoteByTitle = getNoteByTitle;
module.exports.getAllCategories = getAllCategories;
module.exports.titleExists = titleExists;