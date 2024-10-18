import React, { Component } from 'react';
import {Input} from "reactstrap";
import {PlayerTable} from "./PlayerTable";

export class Home extends Component {
  static displayName = Home.name;
  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
      </div>
    );
  }
}
