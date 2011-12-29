import os
from setuptools import setup

def read(fname):
    return open(os.path.join(os.path.dirname(__file__), fname)).read()

setup(
    name = "pacmang2",
    version = "0.0.1",
    author = "Gaetan Gourdin",
    author_email = "bouleetbil@frogdev.info",
    description = ("Python binding for pacman-g2"),
    license = "GPL2",
    keywords = "pacman-g2",
    url = "http://www.frugalware.org",
    packages=['pacmang2'],
    long_description=read('README'),
    classifiers=[
        "Development Status :: Alpha",
        "Topic :: Software Development :: Libraries :: Python Module",
        "License :: GPL2,"
    ],
)

