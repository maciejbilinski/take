<script>

export default {
    data() {
        return {
            newPersonfirstname: '',
            newPersonLastname: '',
            people: [],
        }
    },
    computed: {
        peopleCount() {
            return this.people.length;
        }
    },
    methods: {
        push() {
            this.people.push({
                firstname: this.newPersonfirstname,
                lastname: this.newPersonLastname
            });
            this.newPersonfirstname = '';
            this.newPersonLastname = '';
        }
    }
}
</script>

<template>
    <h2>Add new person</h2>
    <form @submit.prevent="push">
        <input type="text" v-model="newPersonfirstname" placeholder="Firstname"/>
        <br />
        <input type="text" v-model="newPersonLastname" placeholder="Lastname"/>
        <br />
        <input type="submit" value="Add new person" />
    </form>
    <br />
    <h2>
        People
    </h2>
    <ul>
        <li v-for="person in people">{{ person.firstname }} {{ person.lastname }}</li>
    </ul>
    <br />
    <p>Number of people: {{ peopleCount }}</p>
</template>
