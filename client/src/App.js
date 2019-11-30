import React, {Component} from 'react';
import logo from './logo.svg';
import './App.css';


class App extends Component{

  constructor(props){
    super(props);
    this.SearchPanel = this.SearchPanel.bind(this);
    this.GetNotes = this.GetNotes.bind(this);
    this.GetCategories = this.GetCategories.bind(this);
    
    this.state = {
      //page, from, to, category,
      page : 1,
      pageSize: 5,
      category: localStorage.getItem('category') !== undefined ? localStorage.getItem('category') : "",
      from: localStorage.getItem('from') !== undefined ? localStorage.getItem('from') : "",
      to: localStorage.getItem('to') !== undefined ? localStorage.getItem('to') : "",
      notes: []
    };
    
  }
  
  componentDidMount() {
    this.callBackend()
        .then(data => this.setState(data))
        .then(() => console.log(this.state))
        .catch(err => console.log(err));
    this.disableButtons();
  }
  
  disableButtons = () => {
    const maxPage = Math.ceil(this.state.total / this.state.pageSize);
    if(this.state.page === maxPage){
      document.getElementById('next').disabled = true;
    }
    
    if(this.state.page === 1){
      document.getElementById('previous').disabled = true;
    }
    
  };
  
  callBackend = async() => {
    const response = await fetch('/home');
    console.log(response);
    const body = await response.json();
    console.log(response.body);
    console.log(body);
    return body;
  };

  GetNotes(){
    {/*let ns = [*/}
    {/*  {*/}
    //     "title": "kolejna notka",
    //     "description": "tu wpisz treść",
    //     "isMarkdownFile": true,
    //     "formatted_date": "2020-09-20",
    //     "categories": [
    //       "pierwsza",
    //       "druga"
    {/*    ]*/}
    {/*  },*/}
    {/*  {*/}
    //     "title": "notka",
    //     "description": "treść notki",
    //     "isMarkdownFile": false,
    //     "formatted_date": "2019-09-18",
    //     "categories": [
    //       "pierwsza",
    //       "druga"
    //     ]
    //   },
    //   {
    //     "title": "test",
    //     "description": "tu wpisz treść",
    //     "isMarkdownFile": true,
    //     "formatted_date": "2017-11-11",
    //     "categories": [
    //       "pierwsza",
    //       "trzecia"
    //     ]
    //   },
    //   {
    //     "title": "zadanie",
    //     "description": "tu wpisz treść zadania",
    //     "isMarkdownFile": false,
    //     "formatted_date": "1970-01-01",
    //     "categories": [
    //       "pierwsza",
    //       "czwarta kategoria"
    //     ]
    //   }
    // ];
    let ns = this.state.notes;
    let notes = [];
    
    for(let i=0; i<ns.length; i++){
      notes.push(<tr>
        <td>{ns[i].formattedDate}</td>
        <td>{ns[i].title}</td>
        <td><button className="btn btn-primary">Edit</button></td>
        <td><button className="btn btn-danger">Delete</button></td>
      </tr>);
    }
    
    return notes;
  }

  
  GetCategories(){
    let cats = ["1", "2", "3"];
    let categories = [<option> </option>];
    
    for(let category of cats){
      categories.push(<option onClick={this.SetCategory.bind(this, category)}>{category}</option>);
    }
        
    return categories;
  }
  
  SetCategory(category){
    this.setState({
      category: category
    });
  }
  
  GetFilteredData() {
    //call api for filtered notes
  }

  ClearFilters() {
    localStorage.clear();
    //call api for notes
  }
  
  UpdateCache(event){
    localStorage.setItem(event.target.name, event.target.value);
    this.setState({
      [event.target.name]: event.target.value
    })
  }
  
  SearchPanel(){
    return(<form>
              <div className="form-row">
                <div className="col-sm-1">
                </div>
                <div className="col-sm-3">
                  From: <input type="date" name="from" value={this.state.from} onChange={e => this.UpdateCache(e)}/>
                </div>
                <div className="col-sm-2">
                  To: <input type="date" name="to" value={this.state.to} onChange={e => e => this.UpdateCache(e)}/>
                </div>
                <div className="col-sm-2">
                  <select name="category" value={this.state.category} onChange={e => e => this.UpdateCache(e)}>
                    <this.GetCategories />
                  </select>
                </div>
                <div className="col-sm-1">
                  <button type="submit" className="btn btn-primary" onClick={this.GetFilteredData}>Filter</button>
                </div>
                <div className="col-sm-1">
                  <button type="submit" className="btn btn-danger" onClick={this.ClearFilters.bind(this)}>Clear</button>
                </div>
              </div>
            </form>  ); 
  }
  
  render() {
    return (
        <html lang="en">
          <head>
              <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css"
                  integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4"
                  crossOrigin="anonymous"/>
              <title>Notatnik</title>
          </head>
          <body>
            <div className="App" >
              
              <div className="p-5 border">
                <div className="p-3 border">
                  <this.SearchPanel/>
                </div>
                <table className="table table-striped">
                  <tr>
                    <th>Date</th>
                    <th>Title</th>
                    <th> </th>
                    <th> </th>
                  </tr>
                  <this.GetNotes />
                </table> 
                <div className="row">
                  <div className="col-sm-4">
                    <button className="btn btn-success">Add</button>
                  </div>
                  <div className="col-sm-3" style={{textAlign:'right'}}>
                    <button id='previous' className="btn btn-secondary">Previous</button>
                  </div>
                  <div className="col-sm-1" style={{textAlign:'center'}}>
                    page {this.state.page}/{Math.ceil(this.state.total / this.state.pageSize) }
                  </div>
                  <div className="col-sm-3" style={{textAlign:'left'}}>
                    <button id='next' className="btn btn-secondary">Next</button>
                  </div>

                </div>
              </div>
              
              
            </div>
          </body>
        </html>  
    );
   }

 
}


export default App;
