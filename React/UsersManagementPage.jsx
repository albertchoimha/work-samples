import React from 'react'
import UsersManagementService from '../../services/UsersManagementService'
import swal from 'sweetalert'
import UsersManagementData from './UsersManagementData'
import LoadingAnimation from './LoadingAnimation'


class UsersManagementPage extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            usersManagementArr: [],
            isLoading: true
        }
    }

    componentDidMount() {
        UsersManagementService.getAllUM(this.getAllSuccess, this.getAllError)
    }

    getAllSuccess = response => {
        console.log(response)
        this.setState({
            usersManagementArr: response.data.items,
            isLoading: false
        })
    }

    getAllError = error => console.log(error)

    handleCheckboxClick = (userId, roleId, hasRole) => {
        console.log(userId, roleId, hasRole)
        if (hasRole === 1) {
            UsersManagementService.deleteUserRoles(userId, roleId, this.deleteUserRolesSuccess, this.deleteUserRolesError)
        } else if (hasRole === 0) {
            UsersManagementService.insertUserRoles(userId, roleId, this.insertUserRolesSuccess, this.insertUserRolesError)
        }
    }

    insertUserRolesSuccess = response => {
        console.log(response)
        swal({
            title: "Authorized Successfully!",
            icon: "success"
        })
        UsersManagementService.getAllUM(this.getAllSuccess, this.getAllError)
    }

    insertUserRolesError = error => {
        console.log(error)
        swal({
            title: "Error try again!",
            icon: "error"
        })
        window.location.reload()
    }

    deleteUserRolesSuccess = response => {
        console.log(response)
        swal({
            title: "Deleted Successfully!",
            icon: "success"
        })
        UsersManagementService.getAllUM(this.getAllSuccess, this.getAllError)
    }

    deleteUserRolesError = error => {
        console.log(error)
        swal({
            title: "Error try again!",
            icon: "error"
        })
        window.location.reload()
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isLoading ? <LoadingAnimation {...this.state} /> :
                    <div className="table-responsive">
                        <table className="table table-bordered table-hover">
                            <thead className="thead-dark">
                                <tr>
                                    <th>User Id:</th>
                                    <th>Name:</th>
                                    <th>Email:</th>
                                    <th>Role #1:</th>
                                    <th>Role #2:</th>
                                    <th>Role #3:</th>
                                    <th>Role #4:</th>
                                    <th>Role #5:</th>
                                    <th>Role #6:</th>
                                    <th>Role #7:</th>
                                    <th>Role #8:</th>
                                    <th>Role #9:</th>
                                    <th>Role #10:</th>
                                    <th>Role #11:</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.usersManagementArr.map((item) =>
                                    <UsersManagementData
                                        key={item.userId}
                                        user={item}
                                        handleCheckboxClick={this.handleCheckboxClick}
                                    />
                                )}
                            </tbody>
                        </table>
                    </div>
                }
            </React.Fragment>
        )
    }
}

export default UsersManagementPage