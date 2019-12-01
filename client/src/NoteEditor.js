import React, {Component} from 'react';


class NoteEditor extends Component {
    constructor(props){
        super(props);
        console.log(this.props);
        this.GetCategories = this.GetCategories.bind(this);
        this.state = {
            existing: this.props.match.params.title !== undefined,
            title: "",
            isMarkdownFile: false,
            description: "",
            date: "",
            categories: []
        };
        
    }
    
    componentDidMount() {
        const noteTitle = this.props.match.params.title;
        if(noteTitle !== undefined){
            this.loadNote(noteTitle);
        }
    }

    loadNote = title => {
      this.callBackend(`/note/${title}`)
          .then(response => this.setState(response))
          .catch(err => console.log(err));  
    };

    callBackend = async (uri, methodName = "get", bodyObject={}) => {
        let response;
        if(methodName === "post" || methodName === "put"){
            response = await fetch(uri, {
                method: methodName, 
                headers: {"Content-Type": "application/json"}, 
                body: JSON.stringify(bodyObject)
            });
        } else {
            response = await fetch(uri, {method: methodName});
        }
        const jsonResponse = await response.json();
        if(!response.ok){
            throw new Error(jsonResponse);
        }
        return jsonResponse;
    };
    
    addCategory = () => {
        const category = document.getElementById('category').value;
        const categories = this.state.categories;
        if(!categories.find(c => c === category)){
            this.setState(state => {
                const categories = state.categories.concat(category);
                return {
                    categories
                };
            });    
        }
    };
    
    updateState = event => {
      this.setState({
          [event.target.id]: event.target.value
      }); 
    };
    
    updateCheckbox = event => {
      this.setState(state => ({
          isMarkdownFile: !state.isMarkdownFile
      })); 
    };
    
    submitForm = () => {
        const note = this.state;
        let method;
        if(this.state.existing){
            method = 'put'
        } else {
            method = 'post'
        }
        this.callBackend('/note', method, note)
            .then(response => this.props.history.push('/'))
            .catch(err => {
                alert(err);
            });
    };
    
    formatDate = (stringDate, separator) => {
        const date = new Date(stringDate);
        return date.getFullYear() + separator + (date.getMonth() + 1 > 9 ? date.getMonth() + 1 : "0" + (date.getMonth() + 1)) + separator + date.getDate();
    };

    deleteCategory = category => {
        var newCategories = [...this.state.categories];
        const index = newCategories.indexOf(category);
        newCategories.splice(index, 1);
        this.setState({
           categories: newCategories 
        });
    };

    GetCategories(){
        let cats = this.state.categories !== undefined ? this.state.categories : [];
        let categories = [];
        for(let i=0; i < cats.length; i++){
            categories.push(<div className="input-group"> 
                                <input className="list-group-item text-center" name={cats[i]} value={cats[i]} readOnly/>
                                <div className="input-group-append"> 
                                    <button className="btn btn-outline-secondary" type="button" onClick={this.deleteCategory.bind(this, cats[i])}>x</button>
                                </div>
                            </div>);
        }
        return categories;
    };
    
    render(){
        let message = "";
        if(this.state.title === ""){
            message = "Please fill title input!"
        }
        return (
            <div className="App">
                <div className="form-row">
                    <div className="col-sm-1">
                        Title:
                    </div>
                    <div className="col-sm-5">
                        <input id="title" className="form-control" type="text" value={this.state.title} onChange={e => this.updateState(e)}/>
                    </div>
                    <div className="col-sm-1">
                        <input id="isMarkdownFile" className="form-control" type="checkbox" checked={this.state.isMarkdownFile} onClick={e => this.updateCheckbox(e)}/>
                    </div>
                    <div className="col-sm-1">
                        MarkdownFile
                    </div>
                </div>
    
                <div className="form-row">
                    <div className="form-group col-sm-1">
                        Note content:
                    </div>
                    <div className="form-group col-sm-11">
                        <textarea id="description" className="form-control" value={this.state.description} onChange={e => this.updateState(e)}> </textarea>
                    </div>
                </div>
    
                <div className="form-row">
                    <div className="form-group col-sm-1">
                        Categories
                    </div>
                    <div className="form-group col-sm-3">
                        <ul className="list-group">
                            <this.GetCategories />
                        </ul>
                    </div>
                    <div className="form-group col-sm-2 text-center">
                        <p>Category name</p>
                    </div>
                    <div className="form-group col-sm-3">
                        <input id="category" type="text"/>
                    </div>
                    <div className="form-group col-sm-1">
                        <button className="btn btn-success" onClick={this.addCategory.bind(this)}> Add</button>
                    </div>
                </div>
    
                <div className="form-row">
                    <div className="form-group col-sm-1">
                        Date:
                    </div>
                    <div className="form-group col-sm-3" style={{textAlign: 'left'}}>
                        <input type="date" id="date" required value={this.formatDate(this.state.date, "-")} onChange={e => this.updateState(e)}/>
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-group col-sm-12 text-center">
                        <button className="btn btn-primary" disabled={(this.state.title === "" || this.state.date === "")} onClick={this.submitForm.bind(this)}> Ok</button>
                        <a href="/" className="btn btn-secondary"> Cancel</a>
                    </div>
                </div>
                <div className="form-row" style={{textAlign: 'center'}}>
                    <div className="form-group col-sm-12 text-center">
                        {message} 
                    </div>
                </div>
            </div>
    )}
}

export default NoteEditor;