import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Authenticate } from './components/Authenticate';
import { OperationList } from './components/OperationList';
import { NewOperation } from './components/NewOperation';
import { PrivateRoute } from './helpers/PrivateRoute';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Authenticate} />
        <PrivateRoute path='/NewOperation' component={NewOperation} />
        <PrivateRoute path='/OperationList' component={OperationList} />
      </Layout>
    );
  }
}
