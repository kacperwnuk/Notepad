import React, {Component} from 'react';

import './App.css';


class App extends Component {

    constructor(props) {
        super(props);
        this.SearchPanel = this.SearchPanel.bind(this);
        this.GetNotes = this.GetNotes.bind(this);
        this.GetCategories = this.GetCategories.bind(this);

        this.state = this.generateDefaultState();
    }

    generateDefaultState = () => {
        return {
            //page, from, to, category,
            page: 1,
            pageSize: 2,
            maxPages: 1,
            category: localStorage.getItem('category') === null ? "" : localStorage.getItem('category'),
            from: localStorage.getItem('from') === null ? "" : localStorage.getItem('from'),
            to: localStorage.getItem('to') === null ? "" : localStorage.getItem('to'),
            notes: [],
            categories: []
        };
    };

    componentDidMount() {
        this.updateData();

        this.callBackend('/categories')
            .then(data => this.setState(data))
            .catch(err => console.log(err));
    }
    
    getQueryString = () => {
        return this.objToQueryString({
            page: this.state.page,
            pageSize: this.state.pageSize,
            from: this.state.from,
            to: this.state.to,
            category: this.state.category
        });
    };


    objToQueryString = obj => {
        const keyValuePairs = [];
        for (const key in obj) {
            const value = obj[key];
            if (value !== null) {
                keyValuePairs.push(encodeURIComponent(key) + '=' + encodeURIComponent(value));
            }
        }
        return keyValuePairs.join('&');
    };


    callBackend = async (uri, methodName = "get") => {
        const response = await fetch(uri, {method: methodName});
        console.log(response);
        const body = await response.json();
        console.log(response.body);
        console.log(body);
        return body;
    };


    setCategory(category) {
        this.setState({
            category: category
        });
    }

    updateData = () => {
        const queryString = this.getQueryString();
        this.callBackend(`/home?${queryString}`)
            .then(data => this.setState(data))
            .then(() => this.setState({maxPages: Math.ceil(this.state.total / this.state.pageSize)}))
            .catch(err => console.log(err));
    };

    getPage(offset) {
        this.setState((state, props) => ({
            page: state.page + offset
        }), () => {
            this.updateData()
        });
    }
    
    clearFilters() {
        localStorage.clear();
        this.setState((state, props) => ({
            page: 1,
            pageSize: 2,
            maxPages: 1,
            category: "",
            from: "",
            to: ""
        }), () => {
            this.updateData();
        });

    }

    updateCache(event) {
        localStorage.setItem(event.target.id, event.target.value);
        this.setState({
            [event.target.id]: event.target.value
        })
    }

    deleteNote = (title) => {
        this.callBackend(`/note/${title}`, "delete");
        
        this.callBackend('/categories')
            .then(data => this.setState(data))
            .catch(err => console.log(err));
        
        this.updateData();
    };

    formatDate = (stringDate, separator) => {
        const date = new Date(stringDate);
        return date.getFullYear() + separator + (date.getMonth() + 1 > 9 ? date.getMonth() + 1 : "0" + (date.getMonth() + 1)) + separator + date.getDate();
    };
    
    GetCategories() {
        let cats = this.state.categories;
        let categories = [<option> </option>];

        for (let category of cats) {
            categories.push(<option onClick={this.setCategory.bind(this, category)}>{category}</option>);
        }

        return categories;
    }

    GetNotes() {
        let ns = this.state.notes;
        let notes = [];

        for (let i = 0; i < ns.length; i++) {
            notes.push(<tr>
                <td>{this.formatDate(ns[i].date, '/')}</td>
                <td>{ns[i].title}</td>
                <td>
                    <a href={`/noteEditor/${ns[i].title}`} className="btn btn-primary">Edit</a>
                </td>
                <td>
                    <button className="btn btn-danger" onClick={this.deleteNote.bind(this, ns[i].title)}>Delete</button>
                </td>
            </tr>);
        }

        return notes;
    }

    SearchPanel() {
        return (<div className="form-row">
                    <div className="col-sm-1">
                    </div>
                    <div className="col-sm-3">
                        From: <input type="date" id="from" value={this.state.from} onChange={e => this.updateCache(e)}/>
                    </div>
                    <div className="col-sm-2">
                        To: <input type="date" id="to" value={this.state.to} onChange={e => this.updateCache(e)}/>
                    </div>
                    <div className="col-sm-2">
                        <select id="category" value={this.state.category} onChange={e => this.updateCache(e)}>
                            <this.GetCategories/>
                        </select>
                    </div>
                    <div className="col-sm-1">
                        <button className="btn btn-primary" onClick={this.updateData.bind(this)}>Filter</button>
                    </div>
                    <div className="col-sm-1">
                        <button className="btn btn-danger" onClick={this.clearFilters.bind(this)}>Clear</button>
                    </div>
                </div>
        );
    }

    render() {
        return (
            <div className="App">
                <div className="p-5 border">
                    <div className="p-3 border">
                        <this.SearchPanel/>
                    </div>
                    <table className="table table-striped">
                        <thead>
                        <tr>
                            <th>Date</th>
                            <th>Title</th>
                            <th> </th>
                            <th> </th>
                        </tr>
                        </thead>
                        <tbody>
                        <this.GetNotes/>
                        </tbody>
                    </table>
                    <div className="row">
                        <div className="col-sm-4">
                            <a href="/noteEditor" className="btn btn-success">Add</a>
                        </div>
                        <div className="col-sm-3" style={{textAlign: 'right'}}>
                            <button id='previous' className="btn btn-secondary" onClick={this.getPage.bind(this, -1)}
                                    disabled={this.state.page === 1}>Previous
                            </button>
                        </div>
                        <div className="col-sm-1" style={{textAlign: 'center'}}>
                            page {this.state.page}/{this.state.maxPages}
                        </div>
                        <div className="col-sm-3" style={{textAlign: 'left'}}>
                            <button id='next' className="btn btn-secondary" onClick={this.getPage.bind(this, 1)}
                                    disabled={this.state.page === this.state.maxPages}>Next
                            </button>
                        </div>

                    </div>
                   
                </div>
            </div>
        );
    }
}

export default App;

