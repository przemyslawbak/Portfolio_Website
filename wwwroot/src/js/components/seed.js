//seed for React app - dialgs in 'contact' tab.
import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';
import uuid from 'uuid';

var currentHour = new Date().getHours();
var timeOfDay = () => {
    if (currentHour >= 0 && currentHour < 12) return ('morning');
    if (currentHour >= 12 && currentHour < 19) return ('afternoon');
    if (currentHour >= 19 && currentHour < 24) return ('evening');
};
export const questions = [
    {
        //[0] greetings
        id: uuid.v4(),
        question: 'Welcome to my website! I hope you are enjoy your ' + timeOfDay() + '?',
        feedback: [
            { name: '- Yes, it is pretty nice indeed.', goto: 1, id: uuid.v4() },
            { name: '- I have nothing to complain about.', goto: 1, id: uuid.v4() },
            { name: '- I`ve been better.', goto: 2, id: uuid.v4() }
        ]
    },
    {
        //[1] greetings good react
        id: uuid.v4(),
        question: 'I am very happy about it.',
        feedback: [
            { name: 'Continue >', goto: 3, id: uuid.v4() }
        ]
    },
    {
        //[2] greetings bad react
        id: uuid.v4(),
        question: 'I am sorry to hear that.',
        feedback: [
            { name: 'Continue >', goto: 3, id: uuid.v4() }
        ]
    },
    {
        //[3]
        id: uuid.v4(),
        question: 'Let me introduce myself. My name is Przemek, and I builded this website. What is your name?',
        feedback: [
            { name: '- I am not sure if I should answer this question...', goto: 4, id: uuid.v4() },
            { name: '- I do not like talking to strangers. What do you need my name for?', goto: 5, id: uuid.v4() }
        ],
        entry: { label: '- Nice to meet you, Przemek. My name is:', name: 'name', id: uuid.v4() }
    },
    {
        //[4]
        id: uuid.v4(),
        question: 'Do not be afraid, I will not sell this information to the Chinese mafia.',
        feedback: [
            { name: '- I prefer not to answer this question.', goto: 6, id: uuid.v4() }
        ],
        entry: { label: '- My name is:', name: 'name', id: uuid.v4() }
    },
    {
        //[5]
        id: uuid.v4(),
        question: 'I like to know who I am dealing with.',
        feedback: [
            { name: '- I still do not feel confident about it.', goto: 6, id: uuid.v4() }
        ],
        entry: { label: '- My name is:', name: 'name', id: uuid.v4() }
    },
    {
        //[6]
        id: uuid.v4(),
        question: ', tell me what do you think about my website?',
        feedback: [
            { name: '- Actually I really like it, you have done a great job.', goto: 7, opinion: 'v.good', id: uuid.v4() },
            { name: '- It is pretty cool, I like it.', goto: 7, opinion: 'good', id: uuid.v4() },
            { name: '- The website is not bad, but I would change a few things.', goto: 7, opinion: 'average', id: uuid.v4() },
            { name: '- I saw better.', goto: 7, opinion: 'bad', id: uuid.v4() },
            { name: '- It is one of the worst I have ever seen.', goto: 7, opinion: 'v.bad', id: uuid.v4() }
        ]
    },
    {
        //[7]
        id: uuid.v4(),
        question: 'Is there anything you suggest to change here?',
        feedback: [
            { name: '- Actually no, I have nothing in my mind right now.', goto: 8, id: uuid.v4() }
        ],
        entry: { label: '- You could change:', name: 'change', id: uuid.v4() }
    },
    {
        //[8] greetings bad react
        id: uuid.v4(),
        question: 'Great, I appreciate your opinion very much, it means a lot to me.',
        feedback: [
            { name: 'Continue >', goto: 9, id: uuid.v4() }
        ]
    },
    {
        //[9]
        id: uuid.v4(),
        question: 'I will read your comments later. They will help me to improve my website. If you want me to remove them, or if you have any questions, feel free to write or call me. I wish you a successful rest of the day.',
        feedback: []

    }
];

export default {
    questions
};