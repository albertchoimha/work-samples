import React from 'react'
import ProfileDataService from '../../services/ProfileDataService'
import UsersService from '../../services/UsersService'

class ProfilePage extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            userId: "",
            preview: null,
            baseUrl: "",
            fqUrl: "",
            firstName: "",
            middleInitial: "",
            lastName: "",
            userName: "",
            title: "",
            email: "",
            phoneNumber: "",
            bio: "",
            telephones: [],
            gender: ""
        }
    }

    componentDidMount() {
        UsersService.getCurrent(this.getCurrentSuccess, this.getCurrentError)
    }

    getCurrentSuccess = response => {
        this.setState({
            userId: response.data.id
        })
        ProfileDataService.getById(this.state.userId, this.getByIdSuccess, this.getByIdError)
    }

    getCurrentError = error => console.log(error)

    getByIdSuccess = response => this.setState(response.data.item)

    getByIdError = error => console.log(error)

    render() {
        let cellPhoneTypes = {
            cell: [],
            work: [],
            home: []
        }
        let telephones = this.state.telephones
        for (let i = 0; i < telephones.length; i++) {
            let phoneInput =
                <div key={telephones[i].id}>
                    <span>{telephones[i].phoneNumber} {telephones[i].extension ? "x" + telephones[i].extension : ""}</span>
                </div>
            switch (telephones[i].displayName) {
                case "Cell":
                    cellPhoneTypes.cell.push(phoneInput)
                    break;
                case "Work":
                    cellPhoneTypes.work.push(phoneInput)
                    break;
                case "Home":
                    cellPhoneTypes.home.push(phoneInput)
                    break;
                default:
                    break;
            }
        }
        return (
            <div>
                <div className="container-fluid flex-grow-1 container-p-y">
                    <div className="row">
                        <div className="col-xl-4">
                            <div className="card mb-4">
                                <div className="card-body">
                                    <div className="media">
                                        <img src={this.state.fqUrl} alt="" className="ui-w-100 rounded circle" />
                                        <div className="media-body pt-2 ml-3">
                                            <h5 className="mb-2">{this.state.firstName} {this.state.middleInitial && this.state.middleInitial} {this.state.lastName}</h5>
                                            <div className="text-muted">{this.state.title}</div>
                                        </div>
                                    </div>
                                </div>
                                <hr className="border-light m-0" />
                                <div className="card-body">
                                    <div className="mb-3">
                                        <span className="text-muted">Username:</span> {this.state.userName}
                                    </div>
                                    <div className="mb-3">
                                        <span className="text-muted">Email:</span> {this.state.email}
                                    </div>
                                    <div className="mb-3">
                                        <span className="text-muted">Cell Phone:</span> {cellPhoneTypes.cell}
                                    </div>
                                    <div className="mb-3">
                                        <span className="text-muted">Work Phone:</span> {cellPhoneTypes.work}
                                    </div>
                                    <div className="mb-3">
                                        <span className="text-muted">Home Phone:</span> {cellPhoneTypes.home}
                                    </div>
                                    {this.state.gender ?
                                        <div className="mb-3">
                                            <span className="text-muted">Gender:</span> {this.state.gender}
                                        </div> : ""}
                                    <div className="text-muted">
                                        {this.state.bio}
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-xl-8">
                            <div className="card mb-4">
                                <img className="card-img-top mt-4" alt="" src="https://i.ytimg.com/vi/7WCbIjqjHM4/maxresdefault.jpg" />
                                <div className="card-body">
                                    <p>
                                        Aliquam varius euismod lectus, vel consectetur nibh tincidunt vitae. In non dignissim est. Sed eu ligula metus. Vivamus eget quam sit amet risus venenatis laoreet ut vel magna. Sed dui ligula, tincidunt in nunc eu, rhoncus iaculis nisi.
                                        </p>
                                    <p>
                                        Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.
                                        </p>
                                </div>
                            </div>
                            <div className="card mb-4">
                                <img className="card-img-top mt-4" alt="" src="https://images.pexels.com/photos/5443/city-lights-night-rooftop.jpg?cs=srgb&dl=city-hd-wallpaper-lights-5443.jpg&fm=jpg" />
                                <div className="card-body">
                                    <p>
                                        Aliquam varius euismod lectus, vel consectetur nibh tincidunt vitae. In non dignissim est. Sed eu ligula metus. Vivamus eget quam sit amet risus venenatis laoreet ut vel magna. Sed dui ligula, tincidunt in nunc eu, rhoncus iaculis nisi.
                                        </p>
                                    <p>
                                        Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.
                                        </p>
                                </div>
                            </div>
                            <div className="card mb-4">
                                <img className="card-img-top mt-4" alt="" src="https://i.pinimg.com/originals/37/a9/06/37a906be8bd465bb52f092f3f89f9def.jpg" />
                                <div className="card-body">
                                    <p>
                                        Aliquam varius euismod lectus, vel consectetur nibh tincidunt vitae. In non dignissim est. Sed eu ligula metus. Vivamus eget quam sit amet risus venenatis laoreet ut vel magna. Sed dui ligula, tincidunt in nunc eu, rhoncus iaculis nisi.
                                        </p>
                                    <p>
                                        Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.Sed et convallis odio, vel laoreet tellus. Vivamus a leo eu metus porta pulvinar. Pellentesque tristique varius rutrum.
                                        </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        )
    }
}

export default ProfilePage 