import os
from setuptools import setup

def read(fname):
    return open(os.path.join(os.path.dirname(__file__), fname)).read()

setup(
    name = "pyfpmtools",
    version = "0.0.1",
    author = "Gaetan Gourdin",
    author_email = "bouleetbil@frogdev.info",
    description = ("pyfpm tools"),
    license = "GPL2",
    keywords = "pyfpm",
    url = "http://www.frugalware.org",
    packages=['pyfpmtools'],
    long_description=read('README'),
    classifiers=[
        "Development Status :: Alpha",
        "Topic :: Libraries :: Python Module",
        "License :: GPL2,"
    ],
)

