using System;

public class BadRoutineName : Exception {
    public BadRoutineName() {

    }

    public BadRoutineName(string message) : base(message) {

    }

    public BadRoutineName(string message, Exception inner) : base(message, inner) {

    }
}
