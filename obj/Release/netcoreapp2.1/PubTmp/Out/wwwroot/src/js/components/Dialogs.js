import React, { Component } from 'react';
import seed, { questions } from './seed';
//below, get cookie for antiforgery, similar to https://www.w3schools.com/js/js_cookies.asp
function getCookie(name) {
    if (!document.cookie) {
        return null;
    }
    const csrfCookies = document.cookie.split(';')
        .map(c => c.trim())
        .filter(c => c.startsWith(name + '='));
    if (csrfCookies.length === 0) {
        return null;
    }
    return decodeURIComponent(csrfCookies[0].split('=')[1]);
}

function toTitleCase(str) {
    return str.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });
}
//react
class Dialogs extends Component {
    constructor() {
        super();
        //currentDialog - current question, questions - array from seed.js
        this.state = {
            currentDialog: 0,
            questions: [],
            surveyState: {
                name: '',
                change: '',
                opinion: 0
            },
            tempState: {
                name: '',
                change: '',
                opinion: 0
            }
        };
    }
    componentDidMount() {
        //load questions
        this.setState({ questions: seed.questions });
    }
    onInputChange = (evt) => {
        if (evt.target.name == 'name') {
            this.setState({
                tempState: {
                    name: toTitleCase(evt.target.value),
                    change: this.state.surveyState.change,
                    opinion: this.state.surveyState.opinion
                }
            });
        };
        if (evt.target.name == 'change') {
            this.setState({
                tempState: {
                    name: this.state.surveyState.name,
                    change: evt.target.value,
                    opinion: this.state.surveyState.opinion
                }
            });
        }
    };
    onFormSubmit = (evt) => {
        evt.preventDefault();
        this.setState(state => ({
            surveyState: {
                ...state.surveyState,
                ...state.tempState
            }
        }));
        if (this.state.currentDialog > 6) {
            this.setState({
                currentDialog: 8
            });
        } else {
            this.setState({
                currentDialog: 6
            });
        }
    };
    //button clicked
    onButtonClick = (evt) => {
        var elem = document.getElementById('clearMe');
        if (typeof elem !== 'undefined' && elem !== null) {
            document.getElementById("clearMe").reset();
        }
        //declaration
        const btn = evt.target;
        //skip to next
        this.setState(
            {
                currentDialog: btn.getAttribute('goto')
            },
            () => {
                    //if last one, save entity
                if (this.state.currentDialog == 9) {
                    //below using function from top to get the cookies from Startup.cs
                    var csrfToken = getCookie("CSRF-TOKEN");
                    //url for fetch
                    var url = new URL("http://przemyslaw-bak.pl/Survey/AddComment"),
                        params = { Name: this.state.surveyState.name, Change: this.state.surveyState.change, Opinion: this.state.surveyState.opinion };
                    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));
                    fetch(url,
                        {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                "X-CSRF-TOKEN": csrfToken
                            },
                            contentType: "application/json; charset=utf-8",
                            credentials: 'include'
                        }
                    )
                        .then(res => res.json())
                        .then(res => {
                            console.log(res);
                        })
                        .catch(error => {
                            console.error(error);
                        });
                    //reset the state at the end
                    this.setState({
                        surveyState: {
                            name: '',
                            change: '',
                            opinion: 0
                        }
                    });
                }
                if (this.state.currentDialog == 7) {
                    this.setState({
                        surveyState: {
                            name: this.state.surveyState.name,
                            change: this.state.surveyState.change,
                            opinion: btn.getAttribute('opinion')
                        }
                    });
                }
            });
    };
    render() {
        var replyList = questions.map(reply => {
            return (
                reply.feedback.map(singleReply => {
                    return (
                        <div>
                            <button className="btn btn-link dialogButton"
                                key={singleReply.id}
                                name={singleReply.name}
                                opinion={singleReply.opinion}
                                goto={singleReply.goto}
                                onClick={this.onButtonClick}>
                                {singleReply.name}
                            </button>
                            <br/>
                        </div>
                    );
                })
            );
        });
        var write = () => {
            //argument for input
            var toWrite = questions[this.state.currentDialog].entry;
            //if there is entry
            if (questions[this.state.currentDialog].entry)
                return (
                    <form onSubmit={this.onFormSubmit} id="clearMe">
                        <label>{toWrite.label}</label>
                        {' '}
                        <input
                            className="form-control"
                            name={toWrite.name}
                            onChange={this.onInputChange}
                        />
                    </form>
                );
        };
        var imie;
        if (this.state.currentDialog == 6) {
            if (this.state.surveyState.name) {
                imie = this.state.surveyState.name;
            }
            else {
                imie = "Please";
            }
        }
        else {
            imie = "";
        }
        return (
            //questions - pytanie, replyList - lista odpowiedzi
            <div className="App">
                <div className="dialogQuestion">{imie}{questions[this.state.currentDialog].question}</div>
                <br />
                {write()}
                {replyList[this.state.currentDialog]}
                <br /><br />
            </div>);
    }
}
export default Dialogs;