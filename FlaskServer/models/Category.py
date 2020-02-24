from db import db


class Category(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(50), unique=True, nullable=False)

    def json(self):
        return {'id': self.id, 'name': self.name}

    def save(self):
        db.session.add(self)
        db.session.commit()

    def __repr__(self):
        return f"{self.name}"
