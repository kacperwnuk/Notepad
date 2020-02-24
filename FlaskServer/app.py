from sqlite3 import DatabaseError

from flask import Flask, request
from flask_restful import Resource, Api, abort, reqparse
from db import db
from filters.BasicFilter import Filter
from models.Category import Category
from models.Note import Note
from utils import convert_to_date

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///notes.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True
api = Api(app)

parser = reqparse.RequestParser()


class NoteInfo(Resource):
    def get(self, title):
        note = Note.query.filter_by(title=title).first()
        return note.json() if note else abort(404, message="There is no note with this title")

    def delete(self, title):
        note = Note.query.filter_by(title=title).first()
        if note:
            db.session.delete(note)
            db.session.commit()
            return "Note has been deleted."
        else:
            return abort(400, message="There in no note with this title!")


class NoteList(Resource):
    parser.add_argument('title', type=str)
    parser.add_argument('description', type=str)
    parser.add_argument('isMarkdownFile', type=bool)
    parser.add_argument('date', type=str)
    parser.add_argument('categories', type=str, action='append')

    def get(self):
        page = request.args.get('page', default=1, type=int)
        page_size = request.args.get('pageSize', default=5, type=int)
        category = request.args.get('category', default="", type=str)
        start_date = request.args.get('from', default="", type=str)
        end_date = request.args.get('to', default="", type=str)
        note_filter = Filter(page, page_size, category, start_date, end_date)

        notes = Note.query.all()
        filtered_notes = note_filter.run(notes)
        json_notes = [note.json() for note in filtered_notes]
        return {'notes': json_notes,
                'total': len(notes)}

    def post(self):
        data = parser.parse_args()
        try:
            date = convert_to_date(data['date'])
            if date != '':
                note = Note(title=data['title'], description=data['description'], is_markdown=data['isMarkdownFile'],
                            date=date)
            else:
                note = Note(title=data['title'], description=data['description'], is_markdown=data['isMarkdownFile'])

            note.categories = self._get_categories(data['categories'])

            note.save()
            return note.json()
        except (ValueError, DatabaseError) as e:
            db.session.rollback()
            return abort(400, message=f"Your request contains wrong data! {e}")

    def put(self):
        data = parser.parse_args()
        try:
            db_note = Note.query.filter_by(title=data['title']).first()
            db_note.description = data['description']
            db_note.is_markdown = data['isMarkdownFile']
            date = convert_to_date(data['date'])
            db_note.date = date if date != '' else db_note.date
            db_note.categories = self._get_categories(data['categories'])

            db.session.commit()
            return db_note.json()
        except (ValueError, DatabaseError) as e:
            db.session.rollback()
            return abort(400, message=f"Your request contains wrong data! {e}")

    def _get_categories(self, names_list: list):
        categories = []
        if names_list:
            for category_name in names_list:
                existing_category = Category.query.filter_by(name=category_name).first()
                if not existing_category:
                    existing_category = Category(name=category_name)
                    existing_category.save()
                categories.append(existing_category)
        return categories


class CategoryList(Resource):
    def get(self):
        return {'categories': [category.name for category in Category.query.all() if category.notes]}

    def post(self):
        parser.add_argument('name', type=str)
        args = parser.parse_args()
        name = args['name']
        try:
            category = Category(name=name)
            category.save()
            return category.json()
        except:
            return abort(400, message="Name cannot be null!")


api.add_resource(NoteList, '/notes')
api.add_resource(NoteInfo, '/note/<title>')
api.add_resource(CategoryList, '/categories')

if __name__ == '__main__':
    app.run(debug=True)
