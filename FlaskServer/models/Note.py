from datetime import datetime
from db import db

notes_categories = db.Table('notes_categories',
                            db.Column('note_id', db.Integer, db.ForeignKey('note.id'), primary_key=True),
                            db.Column('category_id', db.Integer, db.ForeignKey('category.id'), primary_key=True))


class Note(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    title = db.Column(db.String(50), unique=True, nullable=False)
    description = db.Column(db.String(300), default='')
    date = db.Column(db.DateTime, nullable=False, default=datetime.utcnow)
    is_markdown = db.Column(db.Boolean, default=False)
    categories = db.relationship('Category', secondary=notes_categories, lazy='subquery',
                                 backref=db.backref('notes', lazy=True))

    def json(self):
        return {'id': self.id,
                'title': self.title,
                'description': self.description,
                'date': str(self.date),
                'isMarkdownFile': self.is_markdown,
                'categories': [category.name for category in self.categories]
                }

    def save(self):
        db.session.add(self)
        db.session.commit()

    def __repr__(self):
        return f"{self.id} {self.title} {self.categories}"
