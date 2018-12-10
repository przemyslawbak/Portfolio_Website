import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';
import uuid from 'uuid';
import Dialogs from '../../js/components/Dialogs';


class App extends Component {
    render() {
        return (
            <div className="App">
                <Dialogs/>
            </div> 
        );
    }
}
export default App;